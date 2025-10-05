namespace app;


class User
    {

        public string Email;
        string _password;


        public User(string Username, string Password)
        {
            Email = Username;
            _password = Password;

        }
public bool TryLogin(string username, string password){

    return username == Email && password == _password;
}



    }