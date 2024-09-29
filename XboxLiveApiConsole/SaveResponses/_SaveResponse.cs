namespace ConsoleApp.SaveResponses
{
    internal abstract class SaveResponse
    {
        private string _filePathLocalAppData;

        protected internal const string filePathA = "OpenXbox\\xbox";
        protected internal abstract string nameItem { get; }

        public SaveResponse() => _filePathLocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);//same: "C:\Users\<username>\AppData\Local"

        public string GetFilePath() => Path.Combine(_filePathLocalAppData, filePathA, nameItem);

    }

    internal class SaveOAuth2 : SaveResponse
    {
        protected internal override string nameItem => "1_oauth2_tokens.json";
    }

    internal class SaveProfile : SaveResponse
    {
        protected internal override string nameItem => "Profile.json";
    }

    internal class SaveTitleHub : SaveResponse
    {
        protected internal override string nameItem => "TitleHub.json";
    }

    internal class SaveXau : SaveResponse
    {
        protected internal override string nameItem => "2_xau_tokens.json";

    }

    internal class SaveXsts : SaveResponse
    {
        protected internal override string nameItem => "3_xsts_tokens.json";
    }
}
