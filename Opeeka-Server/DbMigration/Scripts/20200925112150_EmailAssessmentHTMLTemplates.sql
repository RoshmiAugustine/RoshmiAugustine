IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925112150_EmailAssessmentHTMLTemplates')
BEGIN
    ---------------------------Assesment Mail Subject---------------------------------------------------------------
    UPDATE info.Configuration SET VAlue = 'New Assessment Request' WHERE ConfigurationParameterContextID 
        in (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
        WHERE ConfigurationParameterID 
        in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'AssessmentEmailSubject'))
    
    ---------------------------Assesmnet Mail Text HTML---------------------------------------------------------------
    UPDATE info.Configuration set VAlue = '<!DOCTYPE html PUBLIC “-//W3C//DTD XHTML 1.0 Transitional//EN” “https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd”>
<html xmlns=“https://www.w3.org/1999/xhtml”>
<head>
<title>Test Email Sample</title>
<meta http–equiv=“Content-Type” content=“text/html; charset=UTF-8” />
<meta http–equiv=“X-UA-Compatible” content=“IE=edge” />
<meta name=“viewport” content=“width=device-width, initial-scale=1.0 “ />
</head>
<body>
<table style="width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">
    <tbody>
        <tr style="background-color: #227793; height: 80px;">
            <td style="width: 30%;"></td>
            <td style="width: 40%"></td>
            <td style="width: 30%"></td>
        </tr>
        <tr style="background-color: #227793; height: 70px;">
            <td style="width: 30%"></td>
            <th
                style="width: 40%; background-color: white; text-align: center; font-family: Montserrat-bold; font-size: 30px; padding: 30px 0 20px 0;">
                New Assessment Request
            </th>
            <td style="width: 30%;"></td>
        </tr>
        <tr style="background-color: #f4f4f4;">
            <td style="width: 30%;"></td>
            <td style="width: 40%; background-color: #f4f4f4;">
                <table style="background-color: white; width: 100%;  padding: 0 30px;">
                    <tbody>
                        <tr>
                            <td style="text-align: center; padding: 30px 0  30px 0;">
                                Hi {{personname}}, please
                                fill
                                this out before our
                                next
                                appointment. Please click the "Start
                                Assessment" button below to begin the process.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-bottom: 30px;">
                                Your ability to generate a code
                                will
                                expire in
                                {{expiry}} hours.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-bottom: 50px;">
                                <img src="{{applicationUrl}}/assets/icons/doc1.png" alt="Assessment" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-bottom: 50px;">
                                <a style="background-color: #227793; color: white; padding: 15px 30px; border: 0px; border-radius: 25px;
                    font-size: 24px; cursor: pointer;text-decoration: none;" href="{{emailurl}}">Start
                                    Assessment</a>
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
                                <a style="color: #227793; text-decoration: underline;">
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
                                &#169;
                                2020
                                All rights
                                reserved Opeeka Inc.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; color: #a19f9f; font-size: 10px;">
                                01
                                Blue
                                Ravina Road Suite
                                120,
                                Falsom, CA
                                95630
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td style="width: 30%;"></td>
        </tr>
    </tbody>
</table>
</body>
</html>' WHERE ConfigurationParameterContextID 
    in (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'AssessmentEmailText'))
    
    ---------------------------Assesmnet Mail OTP Text HTML---------------------------------------------------------------
    UPDATE info.Configuration SET VAlue = '<!DOCTYPE html PUBLIC “-//W3C//DTD XHTML 1.0 Transitional//EN” “https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd”>
<html xmlns=“https://www.w3.org/1999/xhtml”>
<head>
<title>Test Email Sample</title>
<meta http–equiv=“Content-Type” content=“text/html; charset=UTF-8” />
<meta http–equiv=“X-UA-Compatible” content=“IE=edge” />
<meta name=“viewport” content=“width=device-width, initial-scale=1.0 “ />
</head>
<body>
<table style="width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">
    <tbody>
        <tr style="background-color: #227793; height: 80px;">
            <td style="width: 30%;"></td>
            <td style="width: 40%;"></td>
            <td style="width: 30%;"></td>
        </tr>
        <tr style="background-color: #227793; height: 70px;">
            <td style="width: 30%;"></td>
            <th
                style="width: 40%; background-color: white; text-align: center; font-family: Montserrat-bold; font-size: 30px; padding: 30px 0 20px 0;">
                Verification Code
            </th>
            <td style="width: 30%;"></td>
        </tr>
        <tr style="background-color: #f4f4f4;">
            <td style="width: 30%;"></td>
            <td style="width: 40%; background-color: #f4f4f4;">
                <table style="background-color: white; width: 100%;  padding: 0 30px;">
                    <tbody>
                        <tr>
                            <td style="text-align: center; font-size: 20px; padding: 20px 0  10px 0;">Your Verification
                                Code
                                is:
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: 20px; font-weight: bold; padding: 10px 0 10px 0;">
                                {{otpcode}}</td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding: 20px 0 20px 0;">
                                Please copy this code above and paste it into the website to begin completing your
                                assessment. This code will expire in {{expiry}} hours.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding: 10px 0 80px 0;">
                                <img src="{{applicationUrl}}/assets/icons/key.png" alt="Assessment" />
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
                                <a style="color: #227793; text-decoration: underline;">
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
                                &#169;
                                2020
                                All rights
                                reserved Opeeka Inc.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; color: #a19f9f; font-size: 10px;">
                                01
                                Blue
                                Ravina Road Suite
                                120,
                                Falsom, CA
                                95630
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td style="width: 30%;"></td>
        </tr>
    </tbody>
</table>
</body>
</html>' WHERE ConfigurationParameterContextID 
    in (SELECT ConfigurationParameterContextID from info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'AssessmentOTPEmailText'))
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925112150_EmailAssessmentHTMLTemplates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200925112150_EmailAssessmentHTMLTemplates', N'3.1.4');
END;

GO

