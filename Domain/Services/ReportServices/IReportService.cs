namespace Library.Domain.Services.ReportServices;

public interface IReportService
{
    Task<IEnumerable<Object>> GetMovimentationReports();
    Task<IEnumerable<Object>> GetMovimentationByPeriodReports(DateTime? initialDate, DateTime? finalDate );
    Task<IEnumerable<Object>> GetInventoryReports();
    Task<IEnumerable<Object>> GetLoanReports();
    Task<IEnumerable<Object>> GetLoanByPeriodReports(DateTime? initialDate, DateTime? finalDate);
}
