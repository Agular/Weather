using Hangfire.Common;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using HangfireBasicAuthenticationFilter;
using MongoDB.Driver;
using WebApi.Framework;
using WebApi.Server.Configuration;

namespace WebApi.Server.Extensions;

public static class HangfireExtensions {
	public static void AddCustomHangfire(this IHostApplicationBuilder builder, IMongoClient mongoClient, string databaseName) {
		var configuration = builder.Configuration.GetSection(HangfireConfiguration.SectionKey).Get<HangfireConfiguration>();
		builder.Services.AddHangfire(config => config
			.UseColouredConsoleLogProvider()
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UseFilter(new AutomaticRetryAttribute { Attempts = configuration.MaxRetry })
			.UseMongoStorage(mongoClient, databaseName, new MongoStorageOptions {
				MigrationOptions = new MongoMigrationOptions {
					MigrationStrategy = new DropMongoMigrationStrategy(),
					BackupStrategy = new CollectionMongoBackupStrategy(),
				},
				CheckQueuedJobsStrategy = builder.Environment.EnvironmentName == "Development" ? CheckQueuedJobsStrategy.TailNotificationsCollection : CheckQueuedJobsStrategy.Watch,
				Prefix = "hangfire",
				CheckConnection = true
			})
		);
	}

	public static void AddHangfireDashboard(this WebApplication app) {
		var configuration = app.Configuration.GetSection(HangfireConfiguration.SectionKey).Get<HangfireConfiguration>();
		if (configuration is { DashboardEnabled: true }) {
			app.UseHangfireDashboard(configuration.DashboardUrl, new DashboardOptions {
				Authorization = new[] {
					new HangfireCustomBasicAuthenticationFilter {
						User = configuration.DashboardUsername,
						Pass = configuration.DashboardPassword
					}
				}
			});
		}
	}

	public static void AddHangfireJobs(this WebApplication app) {
		var configuration = app.Configuration.GetSection(HangfireConfiguration.SectionKey).Get<HangfireConfiguration>();
		var manager = app.Services.GetRequiredService<IRecurringJobManager>();
		var logger = app.Services.GetRequiredService<ILogger<Program>>();

		if (configuration == null) {
			logger.LogError("Found no configuration for background jobs.");
			return;
		}

		foreach (var jobConfiguration in configuration.Jobs) {
			logger.LogInformation("Adding Job {name} with {cron}, argsCount= {argsCount}", jobConfiguration.Name, jobConfiguration.Cron, jobConfiguration.Args.IsEmpty() ? 0 : jobConfiguration.Args.Length);
			var type = Type.GetType(jobConfiguration.Type);
			if (type is null) {
				logger.LogError("Job type {type} not found", jobConfiguration.Type);
				continue;
			}

			var method = type.GetMethod(jobConfiguration.Method);
			if (method is null) {
				logger.LogError("Job method {method} not found", jobConfiguration.Method);
				continue;
			}

			var job = jobConfiguration.Args.IsEmpty() ? new Job(type, method) : new Job(type, method, jobConfiguration.Args);
			manager.AddOrUpdate(jobConfiguration.Name, job, jobConfiguration.Cron, new RecurringJobOptions { TimeZone = TimeZoneInfo.Local });
		}
	}
}