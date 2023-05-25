using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace LEFiles.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : ControllerBase
  {

    [HttpGet("RSAInfo")]
    public IActionResult GetRSA()
    {
      var str = Encoding.UTF8.GetBytes("merhaba");
      var rsaKeys = RSA.Create(2048);
      RSA rsa = new RSACryptoServiceProvider(2048);
      var bytePriv = rsa.ExportPkcs8PrivateKey();
      var bytePub = rsa.ExportSubjectPublicKeyInfo();
      string privateKey = Convert.ToBase64String(bytePriv);
      string publicKey = Convert.ToBase64String(bytePub);
      string result = privateKey +" -- "+ publicKey;
      return Ok(result);
    }

  }
}
