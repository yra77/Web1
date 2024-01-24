

using Web1.Models;
using Web1.Services.Repository;


namespace Web1.Services.Auth
{
    internal class Auth : IAuth
    {


        private readonly IRepository _repository;


        public Auth(IRepository repository)
        {
            _repository = repository;
        }


        public async Task<(bool, (string, LoginModel))> AuthAsync(string email, string password)
        {

            string str = null;
            LoginModel loginModel = new LoginModel() { Email = email, Password = password};

                    try
                    {
                       var res = await _repository.GetDataAsync<LoginModel>(loginModel);

                        if (res != null)
                        {
                                str = "No Found password";
                        }
                    }
                    catch
                    {
                        str = "PPPPPPPPPPPPPPPPPPPP";
                    }

            return (false, (str, null));
        }


        //public async Task<(bool, string)> RegistrAsync(Login profile)
        //{

        //    string str = null;


        //    if (_verifyInput.IsValidEmail(profile.Email))
        //    {

        //        if (profile.Name.Length > 2 && _verifyInput.PasswordVerify(profile.Password))
        //        {

        //            try
        //            {

        //                var res = await _restService.GetDataAsync<Login>("Email", profile.Email);

        //                if (res == null && await _restService.InsertAsync<Login>(profile))
        //                {
        //                    _settingsManager.Name = profile.Name;
        //                    _settingsManager.Email = profile.Email;
        //                    _settingsManager.Password = profile.Password;
        //                    _settingsManager.Tel = profile.Tel;
        //                    _settingsManager.Address = profile.Address;
        //                    _settingsManager.IsCircleCart = (profile.CartStatus > 0);
        //                    return (true, str);
        //                }
        //                else
        //                {
        //                    str = Resources.Strings.Resource.Alert_Email1;
        //                }
        //            }
        //            catch
        //            {
        //                str = Resources.Strings.Resource.NotServer;
        //            }
        //        }
        //        else
        //        {
        //            str = Resources.Strings.Resource.Alert_Password2;
        //        }
        //    }
        //    else
        //    {
        //        str = Resources.Strings.Resource.Alert_Email3;
        //    }

        //    return (false, str);
        //}

        //public async Task<bool> UpdateAsync(Login log)
        //{
        //    try
        //    {
        //        var res = await _restService.GetDataAsync<Login>("Email", _settingsManager.Email);

        //        if (res != null)
        //        {
        //            res = log;

        //            return await _restService.UpdateDataAsync(res.Id, res);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Update Error " + e.Message);
        //    }
        //    return false;
        //}

        //private async Task<bool> InsertAsync(Login profile)
        //{
        //    try
        //    {
        //        var res = await _restService.GetDataAsync<Login>("Email", profile.Email);

        //        if (res == null && await _restService.InsertAsync<Login>(profile))
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //        return false;
        //}

    }
}
