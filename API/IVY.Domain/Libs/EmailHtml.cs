namespace IVY.Domain.Libs;
public static class EmailTemplateHtml
{
    public static string GetAccountVerificationEmail(string verifyLink, string userName)
{
    return $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8' />
  <title>Xác nhận tài khoản</title>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
  <table width='100%' style='max-width: 600px; margin: auto; background-color: #fff; padding: 30px; border-radius: 8px;'>
    <tr>
      <td>
        <h2 style='color: #333;'>Chào mừng {userName} đến với [Tên Website]!</h2>
        <p>Cảm ơn bạn đã đăng ký. Vui lòng nhấn vào nút bên dưới để xác nhận tài khoản của bạn.</p>
        <p style='text-align: center; margin: 30px 0;'>
          <a href='{verifyLink}' style='padding: 12px 24px; background-color: #007BFF; color: white; text-decoration: none; border-radius: 5px;'>
            Xác nhận tài khoản
          </a>
        </p>
        <p>Nếu bạn không đăng ký tài khoản này, vui lòng bỏ qua email này.</p>
        <p>Trân trọng,<br />Đội ngũ [Tên Website]</p>
      </td>
    </tr>
  </table>
</body>
</html>";
}
public static string GetResetPasswordEmail(string resetLink, string userName)
{
    return $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8' />
  <title>Đặt lại mật khẩu</title>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
  <table width='100%' style='max-width: 600px; margin: auto; background-color: #fff; padding: 30px; border-radius: 8px;'>
    <tr>
      <td>
        <h2 style='color: #333;'>Xin chào {userName},</h2>
        <p>Chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn.</p>
        <p style='text-align: center; margin: 30px 0;'>
          <a href='{resetLink}' style='padding: 12px 24px; background-color: #28a745; color: white; text-decoration: none; border-radius: 5px;'>
            Đặt lại mật khẩu
          </a>
        </p>
        <p>Nếu bạn không yêu cầu điều này, vui lòng bỏ qua email này.</p>
        <p>Liên kết có hiệu lực trong vòng 15 phút kể từ khi gửi.</p>
        <p>Trân trọng,<br />Đội ngũ [Tên Website]</p>
      </td>
    </tr>
  </table>
</body>
</html>";
}

}