namespace EventManagementApp.Helpers
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email,string emailToken)
        {
            return $@"
<html>
<head>
  <title>Password Reset</title>
</head>
<body>
  <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">
    <tr>
      <td align=""center"" bgcolor=""#f1f1f1"" style=""padding: 40px 0;"">
        <h1 style=""color: #333333;"">Password Reset</h1>
      </td>
    </tr>
    <tr>
      <td align=""center"" style=""padding: 20px;"">
        <p style=""color: #555555;"">Dear User,</p>
        <p style=""color: #555555;"">You have requested to reset your password. Please click the button below to proceed:</p>
        <p>
          <a href=""http://localhost:4200/reset?email={email}&code={emailToken}"" style=""background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none;"">Reset Password</a>
        </p>
        <p style=""color: #555555;"">If you did not request a password reset, please ignore this email.</p>
        <p style=""color: #555555;"">Sincerely,</p>
        <p style=""color: #555555;"">Your Name</p>
      </td>
    </tr>
  </table>
</body>
</html>
";
        }
    }
}
