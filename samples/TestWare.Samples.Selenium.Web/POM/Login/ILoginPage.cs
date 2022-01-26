using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Login
{
    /// <summary>
    /// Encapsulate all Loging busines logic
    /// </summary>
    public interface ILoginPage : ITestWareComponent
    {
        /// <summary>
        /// Script to send User Name
        /// </summary>
        /// <param name="name"></param>
        void EnterUserName(string name);

        /// <summary>
        /// Script to send User Password
        /// </summary>
        /// <param name="name"></param>
        void EnterUserPassword(string password);

        /// <summary>
        /// Click in Login button
        /// </summary>
        void ClickLoginButton();

        /// <summary>
        /// Script to check that the user is at Manager/homepage tab
        /// </summary>
        void CheckUserIsAtHomepage();

        /// <summary>
        /// Check that the user is at Login Page
        /// </summary>
        void CheckUserIsAtLoginpage();

        /// <summary>
        /// Confirm logout pop-up element
        /// </summary>
        void ConfirmLogoutPopup();

        void AcceptLogoutAlert();
    }
}
