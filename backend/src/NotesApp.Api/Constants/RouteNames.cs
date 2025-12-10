namespace NotesApp.Api.Constants;

public static class RouteNames
{
    public const string ApiBase = "api";
    public static class Users
    {
        public const string Resource = "users";
        public const string Base = $"{ApiBase}/{Resource}";


        public const string GetById = "{id:guid}";
        public const string GetAll = $"";
        public const string Create = $"";
        public const string Update = "{id:guid}";
        public const string Delete = "{id:guid}";
    }

    public static class Notes
    {
        public const string Resource = "notes";
        public const string Base = $"{ApiBase}/{Resource}";

        public const string GetById = "{id:guid}";
        public const string GetAll = "";
        public const string Create = "";
        public const string Update = "{id:guid}";
        public const string Delete = "{id:guid}";
    }

    public static class Auth
    {
        public const string Resource = "auth";
        public const string Base = $"{ApiBase}/{Resource}";

        public const string Register = "register";
        public const string Login = "login";
    }
}
