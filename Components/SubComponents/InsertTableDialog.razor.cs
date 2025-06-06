using Radzen;

namespace L4FutureTechBlazor.Components.SubComponents;

public partial class InsertTableDialog
{

    public int Row { get; set; } = 1;
    public int Column { get; set; } = 1;
    public Table? selectedValue { get; set; }

    public void SetTable()
    {
        selectedValue = new Table { Row = Row, Column = Column };
        CloseDialog();
    }

    private void CloseDialog()
    {
        if (Row > 0 && Column > 0)
        {
            DialogService.Close(selectedValue);
        }
        else
        {
            DialogService.Close();
        }
    }
}

public class Table
{
    public int Row { get; set; }
    public int Column { get; set; }
}

