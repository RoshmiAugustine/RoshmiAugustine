IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514012529_EmailTemplateUpdate')
BEGIN
   UPDATE info.Configuration set VAlue = '<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Email Verification</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        crossorigin="anonymous">
    <style type="text/css">
        tr {
            position: relative;
            display: flex;
            flex-direction: row;
            justify-content: center;
            vertical-align: middle;

        }

        tbody td {
            text-align: center;
        }

        td div,
        p,
        a,
        h1 {
            text-align: center;
        }

        .inner-table {
            width: 100% !important;

        }

        #para-1 {
            margin-bottom: 0;
        }

        @media only screen and (max-width: 600px) {
            #para-1 {
                margin: 0 auto;
                width: 160px;
                word-break: normal;
            }
        }
    </style>
</head>

<body style="margin: 0; padding: 0; width: 100%; height: 100%;">
    <table cellspacing="0" cellpadding="0" width="100%" style="margin: 0 auto;text-align: center;">
        <tbody>
            <tr style="background-color: #227793; height: 150px;">
                <td style="text-align: center;padding: 20px; margin: 0 auto;">
                    <img src="{{applicationUrl}}/assets/icons/Logo.png" alt="Assessment" />
                </td>
            </tr>
            <tr style="background-color: #f4f4f4;">
                <td style="background-color: white; margin: 0 auto; margin-top:-3%;">
                    <table class="inner-table" style="margin: 0 auto;">
                        <tbody>
                            <tr style="padding: 30px;display: block;text-align: center;">
                                <td>
                                    <div style="display: block;text-align: center;">
                                        <h1 style="font-size: 25px; font-weight: bold; margin-bottom: 30px;">
                                            Verification Code</h1>
                                        <p style="color:#616161;">
                                            Your Verification Code is:
                                        </p>
                                        <p style="color:black; font-size: 24px;">
                                            {{otpcode}}
                                        </p>
                                        <p style="color:#616161; max-width: 325px; margin: 0 auto;">
                                            Please copy this code above and paste it into the website to begin
                                            completing your
                                            assessment. This code will expire in {{expiry}} minutes.
                                        </p>
                                        <div style="margin-top: 25px;">
                                            <img src="{{applicationUrl}}/assets/icons/key.png" alt="Assessment" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr
                                style="background-color: #e5b008; border-top: 15px solid #f4f4f4; border-bottom: 15px solid #f4f4f4; padding: 15px 20px 15px 20px;display: block; text-align: center;">
                                <td style="display: block;text-align: center;padding: 15px 20px 15px 20px;">
                                    <div style="display: block;text-align: center;">
                                        <p id="para-1"> If you need assistance please contact our</p>
                                        <a href="mailto:support@p-cis.com"
                                            style="color: #227793; text-decoration: underline; cursor:pointer; pointer-events: auto;">
                                            Customer Support Team </a>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: block; text-align: center; padding: 25px;">
                                <td style="font-size: 10px; color:#a19f9f; display: block; text-align: center;">
                                    <div style="display: block;text-align: center;">
                                        <div>
                                            <img style="height: 32px;"
                                                src="{{applicationUrl}}/assets/icons/email-logo.png" alt="Assessment" />
                                        </div>
                                        <div>
                                            &#169; 2021 All rights reserved Opeeka Inc.
                                        </div>
                                        <div style="margin-bottom: 20px;">
                                            81
                                            Blue Ravine Road
                                            Suite 120, Folsom, CA 95630
                                        </div>
                                        <div style="margin-bottom: 20px;">
                                            This email is a system generated message
                                            and the email box is not monitored. <br> To contact us, please email
                                            <a href="mailto:support@p-cis.com"
                                                style=" color:#262325; font-weight: bold; text-decoration: underline;"
                                                target="_blank">support@p-cis.com</a>.
                                        </div>
                                        <div style="margin-bottom: 20px;">
                                            <a href="https://www.opeeka.com/privacy-policy/" target="_blank"
                                                style=" color:#262325; font-weight: bold; text-decoration: underline;">Privacy
                                                Policy</a> |
                                            <a href="https://opeeka.kb.help/" target="_blank"
                                                style=" color:#262325; font-weight: bold; text-decoration: underline;">Support</a>
                                            |
                                            <a href="https://www.opeeka.com/terms-of-use/" target="_blank"
                                                style=" color:#262325; font-weight: bold; text-decoration: underline;">Terms
                                                of Use</a>

                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</body>

</html>' WHERE ConfigurationParameterContextID 
    in (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'AssessmentOTPEmailText'))
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210514012529_EmailTemplateUpdate', N'3.1.4');
END;

GO

