
public class DI
{
    public readonly static DI di = new DI();

    private DI() { }

    public GridGenerator gridGenerator { get; private set; }

    public void SetGridGenerator(GridGenerator gridGenerator) => this.gridGenerator = gridGenerator;

}
