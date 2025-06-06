using Radzen.Blazor;

namespace L4FutureTechBlazor.Models;

public class ExportColumnsContainerModel<TItem>
{
    public List<RadzenDataGridColumn<TItem>> Columns { get; } = [];
    public void Register(RadzenDataGridColumn<TItem> column)
    {
        Columns.Add(column);
    }
}
