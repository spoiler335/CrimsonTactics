
public class DI // Depadency Injector class to hold the Singleton References
{
    public readonly static DI di = new DI();

    private DI() { }

    public GridManager gridManager { get; private set; }

    public void SetGridGenerator(GridManager gridGenerator) => this.gridManager = gridGenerator;

}
