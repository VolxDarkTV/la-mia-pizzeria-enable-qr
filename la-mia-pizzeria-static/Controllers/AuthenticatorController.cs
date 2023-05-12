using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

public class AuthenticatorController : Controller
{
    public IActionResult GenerateQRCode(string secretKey, string userName, string issuer)
    {
        string totpUri = $"otpauth://totp/{Uri.EscapeDataString(issuer)}:{Uri.EscapeDataString(userName)}?secret={Uri.EscapeDataString(secretKey)}&issuer={Uri.EscapeDataString(issuer)}";

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(totpUri, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

        byte[] qrCodeBytes = qrCode.GetGraphic(20); // Dimensione del codice QR

        // Restituisci l'immagine come file di immagine
        return File(qrCodeBytes, "image/png");
    }

    // Resto del codice del controller
}
