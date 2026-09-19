using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRecordManagement.Web.Domain;

namespace StudentRecordManagement.Web.Pages;

public class UnitsModel : PageModel
{
    private readonly Registrar _registrar;

    public UnitsModel(Registrar registrar) => _registrar = registrar;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public List<IGrouping<string, Unit>> UnitsByCourse { get; private set; } = new();

    public void OnGet()
    {
        var query = _registrar.Units.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            var term = Search.Trim();
            query = query.Where(u =>
                u.UnitCode.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                u.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                u.Course.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        UnitsByCourse = query
            .GroupBy(u => u.Course)
            .OrderBy(g => g.Key)
            .ToList();
    }
}
