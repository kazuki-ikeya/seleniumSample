
class Program
{
    static void Main(string[] args)
    {
        string path = args[0];

        var main = new JsonOperate.Actions.JsonOperateMain();
        main.Main(path);
    }
}