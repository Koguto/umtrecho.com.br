using System.Threading.Tasks;


namespace LogDefault
{
    public interface ILogServico
    {
        string Debug(string mensagem, string tag_app);
        string Default(string mensagem, string tag_app);
        string Error(string mensagem, string tag_app);
        string Fatal(string mensagem, string tag_app);
        string Info(string mensagem, string tag_app);
        string Warn(string mensagem, string tag_app);

    }
}