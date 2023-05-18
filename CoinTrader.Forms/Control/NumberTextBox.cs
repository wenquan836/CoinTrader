using CoinTrader.Common.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTrader.Forms.Control
{

    public enum NumberType
    {
        Integer = 0,
        Decimal = 1
    }
    public partial class NumberTextBox : TextBox
    {
        public NumberTextBox()
        {
            InitializeComponent();  
        }

        string myText = "";

        static Regex uncomplete = new Regex("^[\\+\\-]$");
        static Regex regInteger = new Regex("^[\\+\\-]?[0-9]+$");
        static Regex regDecimal = new Regex("^[\\+\\-]?[0-9]+\\.?([0-9]+)?$");


        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        public long LongValue
        {
            get
            {
                long val = 0;
                if(long .TryParse(this.Text, out val))
                {

                }
                return 0;
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return 0;
            }
        }

        bool isInnerChangeText = false;

        protected override void OnTextChanged(EventArgs e)
        {
            if (isInnerChangeText)
                return;

            string txt = this.Text;

            if(string.IsNullOrEmpty(txt))
            {
                //txt = this.Range.Min.ToString();
                myText = "";
                return;
            }
            Regex validateRegex = null;

            switch(this.NumberType)
            {
                case NumberType.Integer:
                    validateRegex = regInteger;
                    break;
                case NumberType.Decimal:
                    validateRegex = regDecimal;
                    break;
            }

            bool changed = false;
            if (!validateRegex.IsMatch(txt))
            {
                txt = myText;
                changed = true;
            }
            else
            {   
                switch (this.NumberType)
                {
                    case NumberType.Integer:
                        var valInt = long.Parse(txt);
                        var valTempInt = valInt;
                        if (Range.Max != 0 && Range.Min != 0)
                        {
                            valTempInt = (int)Math.Min(Range.Max, valTempInt);
                            valTempInt = (int)Math.Max(Range.Min, valTempInt);
                        }
                        txt = valTempInt.ToString();
                        changed = valInt != valTempInt;
                        break;
                    case NumberType.Decimal:
                        var valDecimal = decimal.Parse(txt);
                        var valTempDecimal = valDecimal;
                        if (Range.Max != 0 && Range.Min != 0)
                        {
                            valTempDecimal = Math.Min(Range.Max, valTempDecimal);
                            valTempDecimal = Math.Max(Range.Min, valTempDecimal);
                        }
                        txt = valTempDecimal.ToString();
                        changed = valTempDecimal != valDecimal;
                        break;
                }
            }

            isInnerChangeText = true;
            changed = myText != txt;
            myText = txt;
            Text = txt;
            if(changed)
                this.Select(txt.Length, 0);
            isInnerChangeText = false;

            base.OnTextChanged(e);
        }

        private void ValidateText()
        {
            string txt = this.Text;

            if (string.IsNullOrEmpty(txt))
            {
                //txt = this.Range.Min.ToString();
                myText = "";
                return;
            }
            Regex validateRegex = null;

            switch (this.NumberType)
            {
                case NumberType.Integer:
                    validateRegex = regInteger;
                    break;
                case NumberType.Decimal:
                    validateRegex = regDecimal;
                    break;
            }

            bool changed = false;
            if (!validateRegex.IsMatch(txt))
            {
                txt = myText;
                changed = true;
            }
            else
            {


                switch (this.NumberType)
                {
                    case NumberType.Integer:
                        var valInt = long.Parse(txt);
                        var valTempInt = valInt;
                        if (Range.Max != 0 && Range.Min != 0)
                        {
                            valTempInt = (int)Math.Min(Range.Max, valTempInt);
                            valTempInt = (int)Math.Max(Range.Min, valTempInt);
                        }
                        txt = valTempInt.ToString();
                        changed = valInt != valTempInt;
                        break;
                    case NumberType.Decimal:
                        var valDecimal = decimal.Parse(txt);
                        var valTempDecimal = valDecimal;
                        if (Range.Max != 0 && Range.Min != 0)
                        {
                            valTempDecimal = Math.Min(Range.Max, valTempDecimal);
                            valTempDecimal = Math.Max(Range.Min, valTempDecimal);
                        }
                        txt = valTempDecimal.ToString();
                        changed = valTempDecimal != valDecimal;
                        break;
                }
            }

            isInnerChangeText = true;
            changed = myText != txt;
            myText = txt;
            Text = txt;
            if (changed)
                this.Select(txt.Length, 0);
            isInnerChangeText = false;

        }

        public Range<decimal> Range
        {
            get;set;
        }

        public NumberType NumberType
        {
            get;
            set;
        }
    }
}
