using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amonic.Helpers
{
   public static class Util

    {
        public static ErrorProvider ErrorProvider = new ErrorProvider();
        public static void IsCorreo(this TextBox box)
        {
            box.Validating += (sender, e) =>
            {
                ErrorProvider.SetError(box, "");
                if (box.Text.Trim() == String.Empty)
                {
                    ErrorProvider.SetError(box, "This field is requerid");
                    e.Cancel = true;
                    return;
                }
                try
                {
                    MailAddress mailAddress = new MailAddress(box.Text);
                    if (mailAddress.Address != box.Text)
                    {
                        ErrorProvider.SetError(box, "This field not is email, please to try again");
                        e.Cancel = true;
                    }
                }
                catch (Exception)
                {

                    ErrorProvider.SetError(box, "This field not is email, please to try again");
                    e.Cancel = true;
                }
            };
        }
        public static void Requerido(this TextBox box)
        {
            box.Validating += (sender, e) =>
            {
                ErrorProvider.SetError(box, "");
                if(box.Text.Trim() == String.Empty)
                {
                    ErrorProvider.SetError(box, "This field is requerid");
                    e.Cancel = true;
                }
            };
        }
    }
}
