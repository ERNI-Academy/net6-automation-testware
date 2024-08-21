using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Samples.Suts.SwagLabs.Playwright.Screenplay;

public class Locators
{
    public static Locator LoginForm = new(page => page.Locator(".login_wrapper"));
    public static Locator UserField = new(page => page.Locator("[data-test=\"username\"]"));
    public static Locator PasswordField = new(page => page.Locator("[data-test=\"password\"]"));
    public static Locator SubmitBtn = new(page => page.GetByRole(AriaRole.Button, new() { Name = "LOGIN" }));
    public static Locator Card = new(page => page.Locator(".inventory_item"));
    public static Locator Title = new(page => page.Locator(".inventory_item_name"));
    public static Locator ActionBtn = new(page => page.Locator(".btn_inventory"));
    public static Locator Header = new(page => page.Locator(".product_label"));
}
