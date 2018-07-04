
## Saltydog Cognito Forms -- Using Amazon Cognito from Xamarin Forms

Saltydog.Cognito.Forms is a flexible, styleable set of screens and logic for Sign in, Sign Up, Validation Code, and Change Default Password (for cognito console created users), that makes calls, and responds to the Cognito API. The default navigation between screens is for AWS Cognito settings where validation codes are sent to email or SMS, and not links. 

## Simple Usage

1. Add a reference to the Saltydog.Cognito.Forms nuget package
2. In App.Xaml.cs, use the DefaultNavigator to show the SignIn page:

```
       // Create a default navigator
      
       var navigator = new DefaultNavigator
       {
           Authenticated = Authenticated
       };

       // use the default navigator to create and bind the signin page
       PageModelPair pair = navigator
           .CreatePageModelPair(PageId.SignIn, new ApiCognito(), SessionStore.Instance);

       // Create a navigation page with the signin page
       var navPage = new NavigationPage(pair.Page);

       navigator.Page = pair.Page;
       navigator.Navigation = navPage.Navigation;

       MainPage = navPage;

       MainPage.Title = "Cognito Forms";
```

`Authenticated()` is an async action that is called when the app is authenticated. For example:

```
   protected async Task Authenticated()
   {
       Device.BeginInvokeOnMainThread( () =>
       {
           MainPage = new NavigationPage(new MainPage());
       });
   }
```

See the sample app for an example.

## Advanced Customization

### Styling
The provided pages use several style names so that styling can be provided:

|Style|Used For:|
|:-----|:--------|
|**CognitoButton**|Most buttons|
|**CognitoRegistrationButton**|The button in SignIn.xaml used to bring up registrations.|
|**CognitoEntry**|Most entry fields|
|**CognitoPasswordEntry**|Password fields|
|**CognitoLabel**|Most labels|

For example:

```
    <Application.Resources>
        <ResourceDictionary>            
            <Style x:Key="CognitoButton" TargetType="Button">
                <Setter Property="BackgroundColor" Value="#146FE6" />
                <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                <Setter Property="TextColor" Value="White" />
                <Setter Property="FontSize" Value="24" />
                <Setter Property="HeightRequest" Value="76"/>
            </Style>
            <Style x:Key="CognitoRegistrationButton" TargetType="Button">
                <Setter Property="BackgroundColor" Value="Green" />
                <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                <Setter Property="TextColor" Value="White" />
                <Setter Property="FontSize" Value="24" />
                <Setter Property="HeightRequest" Value="76"/>
            </Style>
            <Style x:Key="CognitoEntry" TargetType="Entry">
                <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                <Setter Property="Margin" Value="0,0,0,12" />
            </Style>
            <Style x:Key="CognitoPasswordEntry" TargetType="Entry">
                <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                <Setter Property="IsPassword" Value="True"/>
                <Setter Property="Margin" Value="0,0,0,12" />
            </Style>
            <Style x:Key="CognitoLabel" TargetType="Label">
                <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                <Setter Property="TextColor" Value="Black" />
                <Setter Property="FontSize" Value="Medium" />
            </Style>            
        </ResourceDictionary>
    </Application.Resources>
```


### Customizing navigation and page creation

Saltydog.Cognito.Forms provides a simple class that handles navigation and implicit page creation. It is simply an abstract interface that responds to events. The interface is defined like this:

```
    public interface ICognitoFormsNavigator
    {
        Task OnResult(CognitoEvent ce, CognitoFormsViewModel prior);
    }
```
Where `CognitoEvent` is some event that has occured in the system.

|CognitoEvent| Details |
|---|---|
**DoSignin**|Perform the sign in operation
**DoSignup**|Perform the signup operation
**Authenticated**|An authenticated successfully occured
**BadUserOrPass**|Invalid password error
**UserNotFound**|No such user found
**RegistrationComplete**|The registration was successful
**AccountVerified**|The account was verified after entering a code
**BadCode**|The code was invalid
**PasswordChangedRequired**|A password change is required
**PasswordUpdated**|The password was successfully updated
**PasswordUpdateFailed**|The password updated failed.
**PasswordRequirementsFailed**|The password entered for signup fails the requirements.
**UserNameAlreadyUsed**|The user name entered for signup has already been used.
**AccountConfirmationRequired**|The account needs to be confirmed.

The default implementation of the navigator, navigates to pages, and puts up warning messages according the CognitoEvent values received. It has two methods, one is the `OnResult` the other is responsible for creating the right page and its corresponding view model. OnResult can be overridden for different navigation patterns, or behaviors.

`PageModelPair CreatePageModelPair(...)` creates both the page and the corresponding ViewModel. This method can be overridden if for different Pages, ViewModels, use of IoC is desired, etc.

Lastly, all of the ViewModels have overridable methods that are called based on the result of the cognito API before calls are made to the navigator. Thus, the navigator pattern could be completely abandoned by subclassing all of the ViewModels and setting the Navigator field to null.

### Strings used in Alerts/Message Boxes

There is a simple interface and default implementation that returns strings for the message boxes. The string values can explicitly be changed on the singleton instance, exposed via the default navigator, or by implementing the `ICognitoString` interface and exposing it via the navigator's property. This only applies if the default navigator is used/subclassed.

### More info

See the blog post at [https://www.saltydogtechnology.com/xamarin-forms-aws-cognito/](https://www.saltydogtechnology.com/xamarin-forms-aws-cognito/) for more information about using basic AWS Cognito APIs in Xamarin.

You can test against the user pool referenced in the sample code. There is a validated user with userid of <code>testit@saltydogtechnology.com</code> and password of: <code>Cognito_2018</code> This sample pool requires that the user name be an email. After registering, check the email to get a code for validating the account. (You can configure AWS Cognito with a different set of requirements.)

Post issues and questions. Contact the author at curtis@saltydogtechnology.com for more involved customizations.





