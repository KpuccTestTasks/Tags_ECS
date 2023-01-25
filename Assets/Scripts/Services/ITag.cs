public interface ITag
{
    int Number { get; }
    void SetupTag(int number, int posX, int posY);
    void UpdatePosition(int x, int y, bool animated);
}