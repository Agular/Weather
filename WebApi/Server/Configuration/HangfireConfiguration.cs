namespace WebApi.Server.Configuration;

public class HangfireConfiguration {
	public const string SectionKey = "Hangfire";
	public int MaxRetry { get; set; }
	public bool DashboardEnabled { get; set; }
	public string DashboardUrl { get; set; }
	public string DashboardUsername { get; set; }
	public string DashboardPassword { get; set; }
	public List<HangfireJobConfiguration> Jobs { get; set; }
}

public class HangfireJobConfiguration {
	public string Name { get; set; }
	public string Type { get; set; }
	public string Method { get; set; }
	public object[] Args { get; set; }
	public string Cron { get; set; }
}