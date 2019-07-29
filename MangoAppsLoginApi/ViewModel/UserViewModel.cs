using System.Linq;
using System.ComponentModel;
using System.Windows.Input;
using MangoAppsLoginApi.Commands;
using MangoAppsLoginApi.RestApi;
using MangoAppsLoginApi.Model;
using System.Windows.Controls;

namespace MangoAppsLoginApi.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private User vmuser;
        private string _domainUrl;
        private ApiHelper apiHelper;
        public ICommand SubmitCommand { get; set; }

        public UserViewModel()
        {
            vmuser = new User();
            SubmitCommand = new Command(SubmitExecute, canExecuteMethod);
            apiHelper = new ApiHelper();
        }

        public string username
        {
            get { return vmuser.username; }
            set
            {
                if (vmuser.username != value)
                {
                    vmuser.username = value;
                    OnPropertyChanged("username");
                    OnPropertyChanged("DomainUrl");
                }
            }
        }
        /* To autogenerate DomainUrl from Email Id*/
        public string DomainUrl
        {
            get
            {
                if (vmuser.username != null)
                {
                    string domain = vmuser.username.Split('@')[1];
                    string domain2 = domain.Split('.').Last();
                    string domain3 = domain.Replace("." + domain2, "");
                    string domain4 = domain3.Replace(".", "-");
                    return _domainUrl = "https://" + domain4 + ".mangopulse.com";
                }
                else
                    return _domainUrl;
            }
            set
            {
                if (_domainUrl != value)
                {
                    _domainUrl = value;
                }
            }
        }     

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool canExecuteMethod(object parameter)
        {
            if (string.IsNullOrEmpty(vmuser.username) || string.IsNullOrEmpty(vmuser.password))
                return false;
            else
                return true;
        }

        private void SubmitExecute(object parameter)
        {
            var password = parameter as PasswordBox;
            string encodedPassword = Common.EncodeToBase64(password.Password.ToString()); // Converting password string into Base64-encoded string
            vmuser.password = encodedPassword;
            vmuser.api_key = "MangoAppForWindows";  // Storing unique api_key as mentioned in request sample
            apiHelper.ConnectToServer(vmuser);
        }

        #region "IDataErrorInfo for input validation"
        public string Error
        {
            get { return null; }
        }

        public string this[string property]
        {
            get
            {
                string result = string.Empty;
                if(property== "username")
                {
                    if (username.Length < 4 || username.Length > 100)
                    {
                        result = "LoginId must have atleast 4 characters and maximum 100 characters";
                    }
                    else if (!Common.IsValidLoginId(username))
                    {
                        result = "LoginId should be in a correct Email format";
                    }
                }             
                return result;
            }
        }
        
        #endregion
    }
}
