using CoinTrader.Common.Classes;
using System;
using System.Text.RegularExpressions;
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

        string recordText = "";

        static Regex uncomplete = new Regex("^[\\+\\-]$");
        static Regex regInteger = new Regex("^[\\+\\-]?[0-9]*$");
        static Regex regDecimal = new Regex("^[\\+\\-]?[0-9]+\\.?([0-9]*)?$");


        bool isInnerChangeText = false;
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
                long.TryParse(this.Text, out var val);
                return val;
            }
        }

        public decimal DecimalValue
        {
            get
            {
                decimal.TryParse(this.Text, out var val);
                return val;
            }
        }


        protected override void OnTextChanged(EventArgs e)
        {
            if (isInnerChangeText)
                return;

            string txt = this.Text;

            if(string.IsNullOrEmpty(txt))
            {
                //txt = this.Range.Min.ToString();
                recordText = "";
                return;
            }

            if(uncomplete.IsMatch(txt))
            {
                recordText = txt;
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

            bool changed;
            if (!validateRegex.IsMatch(txt))
            {
                txt = recordText;
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
                        break;
                }
            }

            isInnerChangeText = true;
            changed = Text != txt;
            recordText = txt;
            Text = txt;
            if(changed)
                this.Select(txt.Length, 0);
            isInnerChangeText = false;

            base.OnTextChanged(e);
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
