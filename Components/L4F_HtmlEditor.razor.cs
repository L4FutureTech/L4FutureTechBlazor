using L4FutureTechBlazor.Components.SubComponents;

using Microsoft.AspNetCore.Components;

using Radzen;
using Radzen.Blazor;

namespace L4FutureTechBlazor.Components;
public partial class L4F_HtmlEditor
{

    public RadzenHtmlEditor htmlEditor { get; set; } = null!;

    [Parameter]
    public HtmlContent htmlValue { get; set; } = new HtmlContent();

    [Parameter]
    public string Title { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        if (string.IsNullOrEmpty(Title))
        {
            Title = LOC["Add new comment"];
        }
        if (DialogService is not null)
        {
            DialogService.OnOpen += Open;
            DialogService.OnClose += Close;
        }
    }

    public void OnChange(string htmlContent)
    {
        htmlContent = "<style>img { max-width: 100%; height: auto; }</style>" + htmlContent;
        htmlValue.Value = htmlContent;
    }

    public async Task OnExecute(HtmlEditorExecuteEventArgs args)
    {
        if (args.CommandName == "InsertTable")
        {
            _ = new Table();
            Table? result = await OpenInsertTabledialog();
            if (result is not null)
            {
                _ = InsertTable(args, result.Columns, result.Rows);
            }
        }
    }

    public async Task<Table?> OpenInsertTabledialog()
    {
        if (DialogService is not null)
        {
            dynamic result = await DialogService.OpenAsync<InsertTableDialog>("Tabelle erstellen",
            [],
            new DialogOptions() { Width = "25%", Height = "28%", Resizable = false, Draggable = false });
            Table table = new();
            if (result is null)
            {
                return null;
            }
            table.Columns = result.Column;
            table.Rows = result.Row;
            return table;
        }
        return null;
    }

    private async Task InsertTable(HtmlEditorExecuteEventArgs args, int columns, int rows)
    {
        string generatedRows = string.Empty;
        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            generatedRows += "<tr>";
            for (int columnIndex = 0; columnIndex < columns; columnIndex++)
            {
                generatedRows += "<td class=\"table-cell\" style=\"border-color: rgb(163, 163, 163); border-width: 1pt; vertical-align: top; width: 0.6673in; padding: 4pt;>" +
                     "<p style = \"margin:0in;font-family:Calibri;font-size:11.0pt\"> &nbsp;</ p >" +
                    "</td>";
            }
            generatedRows += "</tr>";
        }

        string table = "<div style=\"direction:ltr\">\r\n\r\n" +
              "<table>\r\n\r\n" +
              "<tbody>\r\n" +
             generatedRows +
              "</tbody>\r\n" +
              "</table>\r\n" +
              "</div>";
        await args.Editor.ExecuteCommandAsync(HtmlEditorCommands.InsertHtml, table);
    }

    private void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
    {
    }

    private void Close(dynamic result)
    {
    }

    public class Table
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
    }
}

public class HtmlContent
{
    public string? Value { get; set; }
}