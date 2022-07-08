﻿IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201126140208_SignUpEmailFix')
BEGIN 
    ---------------------------Assesmnet Mail Text HTML---------------------------------------------------------------
    UPDATE info.Configuration set Value = '<!DOCTYPE html PUBLIC “-//W3C//DTD XHTML 1.0 Transitional//EN” “https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd”>
<html xmlns=“https://www.w3.org/1999/xhtml”>
<head>
<title>Test Email Sample</title>
<meta http–equiv=“Content-Type” content=“text/html; charset=UTF-8” />
<meta http–equiv=“X-UA-Compatible” content=“IE=edge” />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<link href="https://fonts.googleapis.com/css?family=Montserrat" rel=''stylesheet''>
</head>
<style>
table{
    font-family: ''Montserrat'';
}
.header{
    font-family: ''Montserrat'';
}
@media only screen and (max-width: 600px) {
    .hideWidth{
        width: 19% !important;
    }
    .contentWidth{
        width: 81% !important;
    }
    .btn{
        font-size: 16px !important;
        padding: 15px 24px !important;
    }
}
</style>

<body>
<table style="width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">
    <tbody>
        <tr style="background-color: #227793; height: 80px;">
            <td style="width: 25%;" class="hideWidth"></td>
            <td style="width: 50%" class="contentWidth"></td>
            <td style="width: 25%" class="hideWidth"></td>
        </tr>
        <tr style="background-color: #227793; height: 70px;">
            <td style="width: 25%" class="hideWidth"></td>
            <td
                style="width: 50%; background-color: white; text-align: center; padding: 30px 0 20px 0;"
                class="contentWidth header">
                <h1 style="Margin:0;line-height:36px;mso-line-height-rule:exactly;font-family:lato, ''helvetica neue'', helvetica, arial, sans-serif;font-size:30px;font-style:normal;font-weight:normal;color:#111111">
                    Sign Up
                </h1>               
            </td>

            <td style="Margin:0;padding-bottom:5px;padding-left:30px;padding-right:30px;padding-top:35px">
            <td style="width: 25%;" class="hideWidth"></td>
        </tr>
        <tr style="background-color: #f4f4f4;">
            <td style="width: 25%;" class="hideWidth"></td>
            <td style="width: 50%; background-color: #f4f4f4;" class="contentWidth">
                <table style="background-color: white; width: 100%;  padding: 0 30px;">
                    <tbody>
                        <tr>
                            <td style="text-align: center; padding: 30px 0  30px 0;">
                                Hi {{personname}}, Please click on the "Sign Up" button below and use the one-time password provided to complete the Sign Up process.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-bottom: 30px;">
                                Your Temporary Password is <b> {{temporarypassword}} </b>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-bottom: 50px;">
                                <img src="{{applicationUrl}}/assets/icons/key.png" alt="SignUp Here" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-bottom: 50px;">
                                <a class="btn" style="background-color: #227793; color: white; padding: 15px 30px; border: 0px; border-radius: 25px;
                    font-size: 20px; cursor: pointer;text-decoration: none;" href="{{signupurl}}">Sign Up</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table cellspacing="0" cellpadding="0"
                    style="background-color: #e5b008; width: 100%; padding: 25px 0px; margin-top:15px;">
                    <tbody>
                        <tr>
                            <td style="text-align: center;">
                                If you need assistance please
                                contact
                                our
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <a href="mailto:support@p-cis.com" style="color: #227793; text-decoration: underline; cursor:pointer; pointer-events: auto;">
                                    Customer
                                    Support Team
                                </a>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <table cellspacing="0" cellpadding="0" style="width: 100%; padding:0 0 20px 0">
                    <tbody>
                        <tr>
                            <td style="text-align: center; padding:10px 0 0 0">
                                <img style="height: 32px;" src="{{applicationUrl}}/assets/icons/email-logo.png"
                                    alt="Assessment" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; color: #a19f9f; font-size: 10px; margin-bottom: 4px;">
                                &#169; 2020 All rights reserved. Opeeka Inc.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; color: #a19f9f; font-size: 10px;">
                                81 Blue Ravine Road Suite 120, Folsom, CA 95630
                            </td>
                        </tr>
						<tr style="border-collapse:collapse"> 
						  <td align="center" class="es-m-txt-c" style="padding:0;Margin:0;padding-top:20px"><p style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:11px;font-family:lato, ''helvetica neue'', helvetica, arial, sans-serif;line-height:17px;color:#666666">This email is a&nbsp;system generated message and the email box is not monitored.</p><p style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:11px;font-family:lato, ''helvetica neue'', helvetica, arial, sans-serif;line-height:17px;color:#666666">To contact us, please email&nbsp;<a href="mailto:support@p-cis.com" target="_blank" style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, ''helvetica neue'', helvetica, arial, sans-serif;font-size:11px;text-decoration:underline;color:#111111">support@p-cis.com</a>.<br><br>&nbsp;<a href="https://www.opeeka.com/privacy-policy/" target="_blank" style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, ''helvetica neue'', helvetica, arial, sans-serif;font-size:11px;text-decoration:underline;color:#111111">Privacy Policy</a>&nbsp;| <a target="_blank" style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, ''helvetica neue'', helvetica, arial, sans-serif;font-size:11px;text-decoration:underline;color:#111111" href="https://www.opeeka.com/contact/">Support</a> | <a target="_blank" style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, ''helvetica neue'', helvetica, arial, sans-serif;font-size:11px;text-decoration:underline;color:#111111" href="https://www.opeeka.com/terms-of-use/">Terms of Use</a></p></td> 
						</tr> 
                    </tbody>
                </table>
            </td>
            <td style="width: 25%;" class="hideWidth"></td>
        </tr>
    </tbody>
</table>
</body>
</html>' WHERE ConfigurationParameterContextID 
    in (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'SignupEmailText'))
END;
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201126140208_SignUpEmailFix', N'3.1.4');
END;

GO

