using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace TraderForPoe
{

    public class TradeItem
    {
        private static List<TradeItem> lstTradeItems = new List<TradeItem>();

        public TradeTypes TradeType
        {
            get;
            set;
        }

        public string Customer
        {
            get;
            set;
        }

        public string Item
        {
            get;
            set;
        }
        public Currency ItemCurrency
        {
            get;
            set;
        }

        public string ItemCurrencyQuant
        {
            get;
            set;
        }

        public BitmapImage ItemCurrencyBitmap
        {
            get;
            set;
        }
        public string Price
        {
            get;
            set;
        }

        public string AdditionalText
        {
            get;
            set;
        }

        public string League { get; set; }

        public string Stash
        {
            get;
            set;
        }

        public Point StashPosition
        {
            get;
            set;
        }

        public string WhisperMessage
        {
            get;
            set;
        }

        public Currency PriceCurrency { get; set; }

        public BitmapImage PriceCurrencyBitmap { get; set; }


        public bool ItemIsCurrency { get; set; }

        public enum TradeTypes { BUY, SELL };

        public enum Currency { CHAOS, ALCHCHEMY, ALTERATION, ANCIENT, ANNULMENT, APPRENTICE_SEXTANT, ARMOUR_SCRAP, AUGMENTATION, BAUBLE, BESTIARY_ORB, BINDING_ORB, BLACKSMITH_WHETSTONE, BLESSING_CHAYULAH, BLESSING_ESH, BLESSING_TUL, BLESSING_UUL, BLESSING_XOPH, BLESSE, CHANCE, CHISEL, CHROM, DIVINE, ENGINEER, ETERNAL, EXALTED, FUSING, GEMCUTTERS, HARBINGER_ORB, HORIZON_ORB, IMPRINTED_BESTIARY, JEWELLER, JOURNEYMAN_SEXTANT, MASTER_SEXTANT, MIRROR, PORTAL, REGAL, REGRET, SCOUR, SILVER, SPLINTER_CHAYULA, SPLINTER_ESH, SPLINTER_TUL, SPLINTER_UUL, SPLINTER_XOPH, TRANSMUTE, VAAL, WISDOM, DIVINE_VESSEL, OFFERING_GODDESS, SACRIFICE_DAWN, SACRIFICE_DUSK, SACRIFICE_MIDNIGHT, SACRIFICE_NOON, PERANDUS_COIN };

        Regex poeTradeRegex = new Regex("@(.*) (.*): Hi, I would like to buy your (.*) listed for (.*) in (.*) [(]stash tab \"(.*)[\"]; position: left ([0-9]*), top ([0-9]*)[)](.*)");
        Regex poeTradeNoLocationRegex = new Regex("@(.*) (.*): Hi, I would like to buy your (.*) listed for (.*) in (.*)");
        Regex poeTradeUnpricedRegex = new Regex("@(.*) (.*): Hi, I would like to buy your (.*) in (.*) [(]stash tab \"(.*)[\"]; position: left ([0-9]*), top ([0-9]*)[)](.*)");
        Regex poeTradeCurrencyRegex = new Regex("@(.*) (.*): Hi, I'd like to buy your (.*) for my (.*) in (.*).(.*)");

        //English Trades
        Regex poeTradeRegexENG = new Regex("@(.*) (.*): Hi, I would like to buy your (.*) listed for (.*) in (.*) [(]stash tab \"(.*)[\"]; position: left ([0-9]*), top ([0-9]*)[)](.*)");
        Regex poeTradeNoLocationRegexENG = new Regex("@(.*) (.*): Hi, I would like to buy your (.*) listed for (.*) in (.*)");
        Regex poeTradeUnpricedRegexENG = new Regex("@(.*) (.*): Hi, I would like to buy your (.*) in (.*) [(]stash tab \"(.*)[\"]; position: left ([0-9]*), top ([0-9]*)[)](.*)");
        Regex poeTradeCurrencyRegexENG = new Regex("@(.*) (.*): Hi, I'd like to buy your (.*) for my (.*) in (.*).(.*)");
        //Portuguese Trades
        Regex poeTradeRegexPOR = new Regex("@(.*) (.*): Olá, eu gostaria de comprar seu (.*) listado por (.*) na (.*) [(]aba do baú: \"(.*)[\"]; posição: esquerda ([0-9]*), topo ([0-9]*)[)](.*)");
        Regex poeTradeNoLocationRegexPOR = new Regex("@(.*) Olá, eu gostaria de comprar seu (.*) listado por (.*) na (.*)");
        Regex poeTradeUnpricedRegexPOR = new Regex("@(.*) (.*): Olá, eu gostaria de comprar o seu item (.*) na (.*) [(]aba do baú: \"(.*)[\"]; posição: esquerda ([0-9]*), topo ([0-9]*)[)](.*)");
        Regex poeTradeCurrencyRegexPOR = new Regex("@(.*) (.*): Olá, eu gostaria de comprar seu (.*) por meu(s) (.*) na (.*).(.*)");
        //Russian Trades
        Regex poeTradeRegexRUS = new Regex("@(.*) (.*): Здравствуйте, хочу купить у вас (.*) за (.*) в лиге (.*) [(]секция \"(.*)[\"]; позиция: ([0-9]*) столбец, ([0-9]*) ряд[)](.*)");
        Regex poeTradeNoLocationRegexRUS = new Regex("@(.*) (.*): Здравствуйте, хочу купить у вас (.*) за (.*) в лиге (.*)");
        Regex poeTradeUnpricedRegexRUS = new Regex("@(.*) (.*): Здравствуйте, хочу купить у вас (.*) в лиге (.*) [(]секция \"(.*)[\"]; позиция: ([0-9]*) столбец, ([0-9]*) ряд[)](.*)");
        Regex poeTradeCurrencyRegexRUS = new Regex("@(.*) (.*): Здравствуйте, хочу купить у вас (.*) за (.*) в лиге (.*).(.*)");
        //Thai Trades
        Regex poeTradeRegexTHA = new Regex("@(.*) (.*): สวัสดี เราต้องการซื้อ (.*) ที่คุณตั้งขายไว้ในราคา (.*) ในลีก (.*) [(]แท็บ \"(.*)[\"] ตำแหน่ง: ซ้าย ([0-9]*), บน ([0-9]*)[)](.*)");
        Regex poeTradeNoLocationRegexTHA = new Regex("@(.*) (.*): สวัสดี เราต้องการซื้อ (.*) ที่คุณตั้งขายไว้ในราคา (.*) ในลีก (.*)");
        Regex poeTradeUnpricedRegexTHA = new Regex("@(.*) (.*): สวัสดี เราต้องการซื้อ (.*) ที่คุณตั้งขายไว้ในลีก (.*) [(]แท็บ \"(.*)[\"] ตำแหน่ง: ซ้าย ([0-9]*), บน ([0-9]*)[)](.*)");
        Regex poeTradeCurrencyRegexTHA = new Regex("@(.*) (.*): สวัสดี เราต้องการแลก (.*) ของเรากับ (.*) ของคุณในลีก (.*).(.*)");
        //German Trades
        Regex poeTradeRegexGER = new Regex("@(.*) (.*): Hi, ich möchte '(.*)' zum angebotenen Preis von (.*) in der (.*)-Liga kaufen [(]Truhenfach \"(.*)[\"]; Position: ([0-9]*) von links, ([0-9]*) von oben[)](.*)");
        Regex poeTradeNoLocationRegexGER = new Regex("@(.*) (.*): Hi, ich möchte '(.*)' zum angebotenen Preis von (.*) in der (.*)-Liga kaufen");
        Regex poeTradeUnpricedRegexGER = new Regex("@(.*) (.*): Hi, ich möchte '(.*)' in der (.*)-Liga kaufen [(]Truhenfach \"(.*)[\"]; Position: ([0-9]*) von links, ([0-9]*) von oben[)](.*)");
        Regex poeTradeCurrencyRegexGER = new Regex("@(.*) (.*): Hi, ich möchte dein (.*) für mein (.*) in der (.*)-Liga kaufen(.*)");
        //French Trades
        Regex poeTradeRegexFRE = new Regex("@(.*) (.*): Bonjour, je souhaiterais t'acheter (.*) pour (.*) dans la ligue (.*) [(]onglet de réserve \"(.*)[\"] ; ([0-9]*)e en partant de la gauche, ([0-9]*)e en partant du haut[)](.*)");
        Regex poeTradeNoLocationRegexFRE = new Regex("@(.*) (.*): Bonjour, je souhaiterais t'acheter (.*) pour (.*) dans la ligue (.*)");
        Regex poeTradeUnpricedRegexFRE = new Regex("@(.*) (.*): Bonjour, je souhaiterais t'acheter (.*) dans la ligue (.*) [(]onglet de réserve \"(.*)[\"] ; ([0-9]*)e en partant de la gauche, ([0-9]*)e en partant du haut[)](.*)");
        Regex poeTradeCurrencyRegexFRE = new Regex("@(.*) (.*): Salut, je voudrais t'acheter (.*) contre (.*) [(]ligue (.*)[)](.*)");
        //Spanish Trades
        Regex poeTradeRegexSPA = new Regex("@(.*) (.*): Hola, quisiera comprar tu (.*) listado por (.*) en (.*) [(]pestaña de alijo \"(.*)[\"]; posición: izquierda ([0-9]*), arriba ([0-9]*)[)](.*)");
        Regex poeTradeNoLocationRegexSPA = new Regex("@(.*) (.*): Hola, quisiera comprar tu (.*) listado por (.*) en (.*)");
        Regex poeTradeUnpricedRegexSPA = new Regex("@(.*) (.*): Hola, quisiera comprar tu (.*) en (.*) [(]pestaña de alijo \"(.*)[\"]; posición: izquierda ([0-9]*), arriba ([0-9]*)[)](.*)");
        Regex poeTradeCurrencyRegexSPA = new Regex("@(.*) (.*): Hola, quiero comprar tu[(]s[)] (.*) por mi[(]s[)] (.*) en la liga (.*).(.*)");
        //Japanese Trades ; Add exception for group order as league is first
        Regex poeTradeRegexJAP = new Regex("@(.*) (.*): こんにちは、(.*) リーグで (.*) で売っている、あなたの (.*) を購入したいです [(]スタッシュタブ \"(.*)[\"]; 位置: 左から ([0-9]*), 上から ([0-9]*)[)](.*)");
        Regex poeTradeNoLocationRegexJAP = new Regex("@(.*) (.*): こんにちは、(.*) リーグで (.*) で売っている、あなたの (.*) を購入したいです");
        Regex poeTradeUnpricedRegexJAP = new Regex("@(.*) (.*): こんにちは、(.*) リーグであなたの (.*) を購入したいです [(]スタッシュタブ \"(.*)[\"]; 位置: 左から ([0-9]*), 上から ([0-9]*)[)](.*)");
        Regex poeTradeCurrencyRegexJAP = new Regex("@(.*) (.*): こんにちは、私は(.*)であなたの(.*)を私の(.*)で購入したいです。(.*)");
        //Korean Trades ; Add exception for group order as league is second and price first
        Regex poeTradeRegexKOR = new Regex("@(.*) (.*): 안녕하세요, (.*) 올려놓은 (.*) 리그의 (.*)[(]을[)]를 구매하고 싶습니다 [(]보관함 탭 \"(.*)[\"], 위치: 왼쪽 ([0-9]*), 상단 ([0-9]*)[)](.*)");
        Regex poeTradeNoLocationRegexKOR = new Regex("@(.*) (.*): 안녕하세요, (.*) 올려놓은 (.*) 리그의 (.*)[(]을[)]를 구매하고 싶습니다");
        Regex poeTradeUnpricedRegexKOR = new Regex("@(.*) (.*): 안녕하세요, (.*) 리그의 (.*)[(]을[)]를 구매하고 싶습니다 [(]보관함 탭 \"(.*)[\"], 위치: 왼쪽 ([0-9]*), 상단 ([0-9]*)[)](.*)");
        Regex poeTradeCurrencyRegexKOR = new Regex("@(.*) (.*): 안녕하세요, (.*) 리그의 (.*)[(]을[)]를 (.*)[(]으[)]로 구매하고 싶습니다(.*)");

        //PoEAPP (DEPRECATED)
        Regex poeAppRegEx = new Regex("@(.*) (.*): wtb (.*) listed for (.*) in (.*) [(]stash \"(.*)[\"]; left ([0-9]*), top ([0-9]*)[)](.*)");
        Regex poeAppUnpricedRegex = new Regex("@(.*) (.*): wtb (.*) in (.*) [(]stash \"(.*)[\"]; left ([0-9]*), top ([0-9]*)[)](.*)");
        Regex poeAppCurrencyRegex = new Regex("@(.*) (.*): I'd like to buy your (.*) for my (.*) in (.*).(.*)");


        // Constructor
        public TradeItem(string whisper)
        {
            WhisperMessage = whisper;

            SetTradeType(WhisperMessage);

            ParseWhisper(WhisperMessage);

            if (ItemExists(this))
            {
                throw new TradeItemExistsException("Item exists");
            }

            lstTradeItems.Add(this);
        }

        // Set property TradeType
        private void SetTradeType(string whisper)
        {
            if (whisper.Contains("@To"))
            {
                this.TradeType = TradeTypes.BUY;
            }
            else if (whisper.Contains("@From"))
            {
                this.TradeType = TradeTypes.SELL;
            }
        }

        private void ParseWhisper(string whisper)
        {
            if (poeTradeRegexENG.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexENG;
            }
            else if (poeTradeRegexPOR.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexPOR;
            }
            else if (poeTradeRegexRUS.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexRUS;
            }
            else if (poeTradeRegexTHA.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexTHA;
            }
            else if (poeTradeRegexGER.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexGER;
            }
            else if (poeTradeRegexFRE.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexFRE;
            }
            else if (poeTradeRegexSPA.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexSPA;
            }
            else if (poeTradeRegexJAP.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexJAP;
            }
            /*else if (poeTradeRegexKOR.IsMatch(whisper))
            {
                poeTradeRegex = poeTradeRegexKOR;
            }*/
            else if (poeTradeUnpricedRegexENG.IsMatch(whisper) && !whisper.Contains("listed for"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexENG;
            }
            else if (poeTradeUnpricedRegexPOR.IsMatch(whisper) && !whisper.Contains("listado por"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexPOR;
            }
            else if (poeTradeUnpricedRegexRUS.IsMatch(whisper) && !whisper.Contains("Сфера") && !whisper.Contains("сфера"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexRUS;
            }
            else if (poeTradeUnpricedRegexTHA.IsMatch(whisper) && !whisper.Contains("ที่คุณตั้งขายไว้ในราคา"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexTHA;
            }
            else if (poeTradeUnpricedRegexGER.IsMatch(whisper) && !whisper.Contains("angebotenen"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexGER;
            }
            else if (poeTradeUnpricedRegexFRE.IsMatch(whisper) && !whisper.Contains("pour"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexFRE;
            }
            else if (poeTradeUnpricedRegexSPA.IsMatch(whisper) && !whisper.Contains("listado por"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexSPA;
            }
            else if (poeTradeUnpricedRegexJAP.IsMatch(whisper) && !whisper.Contains("で売っている"))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexJAP;
            }
            else if (poeTradeUnpricedRegexKOR.IsMatch(whisper))
            {
                poeTradeUnpricedRegex = poeTradeUnpricedRegexKOR;
            }
            else if (poeTradeNoLocationRegexENG.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexENG;
            }
            else if (poeTradeNoLocationRegexPOR.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexPOR;
            }
            else if (poeTradeNoLocationRegexRUS.IsMatch(whisper) && !whisper.Contains("Сфера") && !whisper.Contains("сфера"))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexRUS;
            }
            else if (poeTradeNoLocationRegexTHA.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexTHA;
            }
            else if (poeTradeNoLocationRegexGER.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexGER;
            }
            else if (poeTradeNoLocationRegexFRE.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexFRE;
            }
            else if (poeTradeNoLocationRegexSPA.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexSPA;
            }
            else if (poeTradeNoLocationRegexJAP.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexJAP;
            }
            else if (poeTradeNoLocationRegexKOR.IsMatch(whisper))
            {
                poeTradeNoLocationRegex = poeTradeNoLocationRegexKOR;
            }
            else if (poeTradeCurrencyRegexENG.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexENG;
            }
            else if (poeTradeCurrencyRegexPOR.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexPOR;
            }
            else if (poeTradeCurrencyRegexRUS.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexRUS;
            }
            else if (poeTradeCurrencyRegexTHA.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexTHA;
            }
            else if (poeTradeCurrencyRegexGER.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexGER;
            }
            else if (poeTradeCurrencyRegexFRE.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexFRE;
            }
            else if (poeTradeCurrencyRegexSPA.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexSPA;
            }
            else if (poeTradeCurrencyRegexJAP.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexJAP;
            }
            else if (poeTradeCurrencyRegexKOR.IsMatch(whisper))
            {
                poeTradeCurrencyRegex = poeTradeCurrencyRegexKOR;
            }

            if (poeTradeRegex.IsMatch(whisper))
            {
                MatchCollection matches = Regex.Matches(whisper, poeTradeRegex.ToString());

                foreach (Match match in matches)
                {
                    // 
                    this.Customer = match.Groups[2].Value;

                    // Set customer
                    this.Customer = match.Groups[2].Value;

                    // Set item
                    this.Item = match.Groups[3].Value;

                    // Set price
                    this.Price = match.Groups[4].Value;

                    this.PriceCurrency = ParseCurrency(this.Price);

                    this.PriceCurrencyBitmap = SetCurrencyBitmap(this.PriceCurrency);

                    // Set league
                    this.League = match.Groups[5].Value;

                    // Set stash
                    this.Stash = match.Groups[6].Value;

                    // Set stash position
                    this.StashPosition = new Point(Convert.ToDouble(match.Groups[7].Value), Convert.ToDouble(match.Groups[8].Value));

                    this.AdditionalText = match.Groups[9].Value;


                }
            }
            else if (poeTradeUnpricedRegex.IsMatch(whisper))
            {
                MatchCollection matches = Regex.Matches(whisper, poeTradeUnpricedRegex.ToString());

                foreach (Match match in matches)
                {
                    // Set customer
                    this.Customer = match.Groups[2].Value;

                    // Set item
                    this.Item = match.Groups[3].Value;

                    // Set league
                    this.League = match.Groups[4].Value;

                    // Set stash
                    this.Stash = match.Groups[5].Value;

                    // Set stash position
                    this.StashPosition = new Point(Convert.ToDouble(match.Groups[6].Value), Convert.ToDouble(match.Groups[7].Value));

                    //this.AdditionalText = match.Groups[8].Value;
                }
            }
            else if (poeTradeNoLocationRegex.IsMatch(whisper))
            {
                MatchCollection matches = Regex.Matches(whisper, poeTradeNoLocationRegex.ToString());

                foreach (Match match in matches)
                {
                    // 
                    this.Customer = match.Groups[2].Value;

                    // Set customer
                    this.Customer = match.Groups[2].Value;

                    // Set item
                    this.Item = match.Groups[3].Value;

                    // Set price
                    this.Price = match.Groups[4].Value;

                    this.PriceCurrency = ParseCurrency(this.Price);

                    this.PriceCurrencyBitmap = SetCurrencyBitmap(this.PriceCurrency);

                    // Set league
                    this.League = match.Groups[5].Value;

                    this.AdditionalText = match.Groups[9].Value;

                }

            }
            else if (poeTradeCurrencyRegex.IsMatch(whisper))
            {
                this.ItemIsCurrency = true;

                MatchCollection matches = Regex.Matches(whisper, poeTradeCurrencyRegex.ToString());

                foreach (Match match in matches)
                {
                    // Set customer
                    this.Customer = match.Groups[2].Value;

                    // Set item
                    this.Item = match.Groups[3].Value;

                    // Set price
                    this.Price = match.Groups[4].Value;

                    try
                    {
                        this.PriceCurrency = ParseCurrency(this.Price);

                        this.PriceCurrencyBitmap = SetCurrencyBitmap(this.PriceCurrency);
                    }
                    catch (Exception)
                    {

                    }

                    //Catch ex. If ex thrown, the item could not be parsed. ItemIsCurrency will be set to false, so the item will be treated as normal item

                    try
                    {

                        this.ItemCurrencyQuant = ExtractPointNumberFromString(this.Item);

                        this.ItemCurrency = ParseCurrency(this.Item);

                        this.ItemCurrencyBitmap = SetCurrencyBitmap(this.ItemCurrency);
                    }
                    catch (Exception)
                    {
                        this.ItemIsCurrency = false;
                    }


                    // Set league
                    this.League = match.Groups[5].Value;

                    this.AdditionalText = match.Groups[6].Value;

                }


            }
            else if (poeAppRegEx.IsMatch(whisper))
            {
                MatchCollection matches = Regex.Matches(whisper, poeAppRegEx.ToString());

                foreach (Match match in matches)
                {
                    // Set customer
                    this.Customer = match.Groups[2].Value;

                    // Set item
                    this.Item = match.Groups[3].Value;

                    // Set price
                    this.Price = match.Groups[4].Value;

                    this.PriceCurrency = ParseCurrency(this.Price);

                    this.PriceCurrencyBitmap = SetCurrencyBitmap(this.PriceCurrency);

                    // Set league
                    this.League = match.Groups[5].Value;

                    // Set stash
                    this.Stash = match.Groups[6].Value;

                    // Set stash position
                    this.StashPosition = new Point(Convert.ToDouble(match.Groups[7].Value), Convert.ToDouble(match.Groups[8].Value));

                    this.AdditionalText = match.Groups[9].Value;
                }
            }
            else if (!whisper.Contains("listed for") && poeAppUnpricedRegex.IsMatch(whisper))
            {
                MatchCollection matches = Regex.Matches(whisper, poeAppUnpricedRegex.ToString());

                foreach (Match match in matches)
                {
                    // Set customer
                    this.Customer = match.Groups[2].Value;

                    // Set item
                    this.Item = match.Groups[3].Value;

                    // Set league
                    this.League = match.Groups[4].Value;

                    // Set stash
                    this.Stash = match.Groups[5].Value;

                    // Set stash position
                    this.StashPosition = new Point(Convert.ToDouble(match.Groups[6].Value), Convert.ToDouble(match.Groups[7].Value));

                    this.AdditionalText = match.Groups[8].Value;
                }
            }
            else if (!whisper.Contains("Hi, ") && poeAppCurrencyRegex.IsMatch(whisper))
            {
                this.ItemIsCurrency = true;

                MatchCollection matches = Regex.Matches(whisper, poeAppCurrencyRegex.ToString());

                foreach (Match match in matches)
                {
                    // Set customer
                    this.Customer = match.Groups[2].Value;

                    // Set item
                    this.Item = match.Groups[3].Value;

                    // Set price
                    this.Price = match.Groups[4].Value;

                    this.PriceCurrency = ParseCurrency(this.Price);

                    this.PriceCurrencyBitmap = SetCurrencyBitmap(this.PriceCurrency);

                    try
                    {
                        this.ItemCurrencyQuant = ExtractPointNumberFromString(this.Item);

                        this.ItemCurrency = ParseCurrency(this.Item);

                        this.ItemCurrencyBitmap = SetCurrencyBitmap(this.ItemCurrency);
                    }
                    catch (Exception)
                    {
                        this.ItemIsCurrency = false;
                    }

                    // Set league
                    this.League = match.Groups[5].Value; ;

                    this.AdditionalText = match.Groups[6].Value;
                }


            }
            else
            {
                throw new NoRegExMatchException("No RegEx match for:\n" + whisper);
            }
        }

        private Currency ParseCurrency(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                string strPrice = s.ToLower();

                if (strPrice.Contains("chaos") && !strPrice.Contains("shard"))
                {
                    return Currency.CHAOS;
                }

                else if (strPrice.Contains("alch") && !strPrice.Contains("shard"))
                {
                    return Currency.ALCHCHEMY;
                }

                else if (strPrice.Contains("alt") && !strPrice.Contains("ex"))
                {
                    return Currency.ALTERATION;
                }

                else if (strPrice.Contains("ancient"))
                {
                    return Currency.ANCIENT;
                }

                else if (strPrice.Contains("annulment") && !strPrice.Contains("shard"))
                {
                    return Currency.ANNULMENT;
                }

                else if (strPrice.Contains("apprentice") && strPrice.Contains("sextant"))
                {
                    return Currency.APPRENTICE_SEXTANT;
                }

                else if (strPrice.Contains("armour") || strPrice.Contains("scrap"))
                {
                    return Currency.ARMOUR_SCRAP;
                }

                else if (strPrice.Contains("aug"))
                {
                    return Currency.AUGMENTATION;
                }

                else if (strPrice.Contains("bauble"))
                {
                    return Currency.BAUBLE;
                }

                else if (strPrice.Contains("bestiary") && strPrice.Contains("orb"))
                {
                    return Currency.BESTIARY_ORB;
                }

                else if (strPrice.Contains("binding") && strPrice.Contains("orb"))
                {
                    return Currency.BINDING_ORB;
                }

                else if (strPrice.Contains("whetstone") || strPrice.Contains("blacksmith"))
                {
                    return Currency.BLACKSMITH_WHETSTONE;
                }

                else if (strPrice.Contains("blessing") && strPrice.Contains("chayulah"))
                {
                    return Currency.BLESSING_CHAYULAH;
                }

                else if (strPrice.Contains("blessing") && strPrice.Contains("esh"))
                {
                    return Currency.BLESSING_ESH;
                }

                else if (strPrice.Contains("blessing") && strPrice.Contains("tul"))
                {
                    return Currency.BLESSING_TUL;
                }

                else if (strPrice.Contains("blessing") && strPrice.Contains("uul"))
                {
                    return Currency.BLESSING_UUL;
                }

                else if (strPrice.Contains("blessing") && strPrice.Contains("xoph"))
                {
                    return Currency.BLESSING_XOPH;
                }

                else if (strPrice.Contains("blesse"))
                {
                    return Currency.BLESSE;
                }

                else if (strPrice.Contains("chance"))
                {
                    return Currency.CHANCE;
                }

                else if (strPrice.Contains("chisel"))
                {
                    return Currency.CHISEL;
                }

                else if (strPrice.Contains("chrom") || strPrice.Contains("chrome"))
                {
                    return Currency.CHROM;
                }

                else if ((strPrice.Contains("divine") || strPrice.Contains("div")) && !strPrice.Contains("vessel"))
                {
                    return Currency.DIVINE;
                }

                else if (strPrice.Contains("engineer") && strPrice.Contains("orb"))
                {
                    return Currency.ENGINEER;
                }

                else if (strPrice.Contains("ete"))
                {
                    return Currency.ETERNAL;
                }

                else if ((strPrice.Contains("ex") || strPrice.Contains("exa") || strPrice.Contains("exalted")) && !strPrice.Contains("shard") && !strPrice.Contains("sext"))
                {
                    return Currency.EXALTED;
                }

                else if (strPrice.Contains("fuse") || strPrice.Contains("fus"))
                {
                    return Currency.FUSING;
                }

                else if (strPrice.Contains("gcp") || strPrice.Contains("gemc"))
                {
                    return Currency.GEMCUTTERS;
                }

                else if (strPrice.Contains("harbinger") && strPrice.Contains("orb"))
                {
                    return Currency.HARBINGER_ORB;
                }

                else if (strPrice.Contains("horizon") && strPrice.Contains("orb"))
                {
                    return Currency.HORIZON_ORB;
                }

                else if (strPrice.Contains("imprinted") && strPrice.Contains("bestiary"))
                {
                    return Currency.IMPRINTED_BESTIARY;
                }

                else if (strPrice.Contains("jew"))
                {
                    return Currency.JEWELLER;
                }

                else if (strPrice.Contains("journeyman") && strPrice.Contains("sextant"))
                {
                    return Currency.JOURNEYMAN_SEXTANT;
                }

                else if (strPrice.Contains("master") && strPrice.Contains("sextant"))
                {
                    return Currency.MASTER_SEXTANT;
                }

                else if (strPrice.Contains("mir") || strPrice.Contains("kal"))
                {
                    return Currency.MIRROR;
                }

                else if (strPrice.Contains("coin"))
                {
                    return Currency.PERANDUS_COIN;
                }

                else if (strPrice.Contains("port"))
                {
                    return Currency.PORTAL;
                }

                else if (strPrice.Contains("rega"))
                {
                    return Currency.REGAL;
                }

                else if (strPrice.Contains("regr"))
                {
                    return Currency.REGRET;
                }

                else if (strPrice.Contains("dawn"))
                {
                    return Currency.SACRIFICE_DAWN;
                }

                else if (strPrice.Contains("dusk"))
                {
                    return Currency.SACRIFICE_DUSK;
                }

                else if (strPrice.Contains("midnight"))
                {
                    return Currency.SACRIFICE_MIDNIGHT;
                }

                else if (strPrice.Contains("noon"))
                {
                    return Currency.SACRIFICE_NOON;
                }

                else if (strPrice.Contains("scour"))
                {
                    return Currency.SCOUR;
                }

                else if (strPrice.Contains("silver"))
                {
                    return Currency.SILVER;
                }

                else if (strPrice.Contains("splinter") && strPrice.Contains("chayula"))
                {
                    return Currency.SPLINTER_CHAYULA;
                }

                else if (strPrice.Contains("splinter") && strPrice.Contains("esh"))
                {
                    return Currency.SPLINTER_ESH;
                }

                else if (strPrice.Contains("splinter") && strPrice.Contains("tul"))
                {
                    return Currency.SPLINTER_TUL;
                }

                else if (strPrice.Contains("splinter") && strPrice.Contains("uul"))
                {
                    return Currency.SPLINTER_UUL;
                }

                else if (strPrice.Contains("splinter") && strPrice.Contains("xoph"))
                {
                    return Currency.SPLINTER_XOPH;
                }

                else if (strPrice.Contains("tra"))
                {
                    return Currency.TRANSMUTE;
                }

                else if (strPrice.Contains("vaal"))
                {
                    return Currency.VAAL;
                }

                else if (strPrice.Contains("wis"))
                {
                    return Currency.WISDOM;
                }

                else if (strPrice.Contains("divine") && strPrice.Contains("vessel"))
                {
                    return Currency.DIVINE_VESSEL;
                }

                else if (strPrice.Contains("offering") || strPrice.Contains("offer"))
                {
                    return Currency.OFFERING_GODDESS;
                }
            }
            throw new NoCurrencyFoundException("Currency " + s + " not found");
        }

        private BitmapImage SetCurrencyBitmap(Currency c)
        {
            switch (c)
            {
                case Currency.CHAOS:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_chaos.png"));
                case Currency.ALCHCHEMY:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_alch.png"));
                case Currency.ALTERATION:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_alt.png"));
                case Currency.ANCIENT:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_ancient.png"));
                case Currency.ANNULMENT:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_annul.png"));
                case Currency.APPRENTICE_SEXTANT:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_appr_carto_sextant.png"));
                case Currency.ARMOUR_SCRAP:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_armour_scrap.png"));
                case Currency.AUGMENTATION:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_aug.png"));
                case Currency.BAUBLE:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_bauble.png"));
                case Currency.BESTIARY_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_bestiary_orb.png"));
                case Currency.BINDING_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_binding.png"));
                case Currency.BLACKSMITH_WHETSTONE:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_black_whetstone.png"));
                case Currency.BLESSING_CHAYULAH:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_bless_chayula.png"));
                case Currency.BLESSING_ESH:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_bless_chayula.png"));
                case Currency.BLESSING_TUL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_bless_tul.png"));
                case Currency.BLESSING_UUL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_bless_uul.png"));
                case Currency.BLESSING_XOPH:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_bless_xoph.png"));
                case Currency.BLESSE:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_blessed.png"));
                case Currency.CHANCE:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_chance.png"));
                case Currency.CHISEL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_chisel.png"));
                case Currency.CHROM:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_chrom.png"));
                case Currency.DIVINE:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_divine.png"));
                case Currency.ENGINEER:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_engineer.png"));
                case Currency.ETERNAL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_eternal.png"));
                case Currency.EXALTED:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_ex.png"));
                case Currency.FUSING:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_fuse.png"));
                case Currency.GEMCUTTERS:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_gcp.png"));
                case Currency.HARBINGER_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_harbinger.png"));
                case Currency.HORIZON_ORB:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_horizon.png"));
                case Currency.IMPRINTED_BESTIARY:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_impr_bestiary.png"));
                case Currency.JEWELLER:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_jew.png"));
                case Currency.JOURNEYMAN_SEXTANT:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_journ_carto_sextant.png"));
                case Currency.MASTER_SEXTANT:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_master_carto_sextant.png"));
                case Currency.MIRROR:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_mirror.png"));
                case Currency.PERANDUS_COIN:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_perandus_coin.png"));
                case Currency.PORTAL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_port.png"));
                case Currency.REGAL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_regal.png"));
                case Currency.REGRET:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_regret.png"));
                case Currency.SACRIFICE_DAWN:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_sacrifice_dawn.png"));
                case Currency.SACRIFICE_DUSK:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_sacrifice_dusk.png"));
                case Currency.SACRIFICE_MIDNIGHT:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_sacrifice_midnight.png"));
                case Currency.SACRIFICE_NOON:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_sacrifice_noon.png"));
                case Currency.SCOUR:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_scour.png"));
                case Currency.SILVER:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_silver.png"));
                case Currency.SPLINTER_CHAYULA:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_splinter_chayula.png"));
                case Currency.SPLINTER_ESH:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_splinter_esh.png"));
                case Currency.SPLINTER_TUL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_splinter_tul.png"));
                case Currency.SPLINTER_UUL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_splinter_uul.png"));
                case Currency.SPLINTER_XOPH:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_splinter_xoph.png"));
                case Currency.TRANSMUTE:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_tra.png"));
                case Currency.VAAL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_vaal.png"));
                case Currency.WISDOM:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_wis.png"));
                case Currency.DIVINE_VESSEL:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_divine_vessel.png"));
                case Currency.OFFERING_GODDESS:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Currency/curr_offering_to_the_goddess.png"));
                default:
                    throw new NoCurrencyBitmapFoundException("No bitmap found for: " + c);
            }
        }

        private static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH! Can not set first char to upper: " + input);
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        /// <summary>
        /// Returns the point number from a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Point number if successfull, null if not</returns>
        public static string ExtractPointNumberFromString(string s)
        {
            var match = Regex.Match(s, @"([-+]?[0-9]*\.?[0-9]+)");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return null;
            }
        }

        public static bool ItemExists(TradeItem ti)
        {
            foreach (var item in lstTradeItems)
            {
                if (item.Item == ti.Item && item.Customer == ti.Customer && item.Price == ti.Price
                    && item.StashPosition == ti.StashPosition && item.TradeType == ti.TradeType)
                {
                    return true;
                }
            }

            return false;

        }

        public static void RemoveItemFromList(TradeItem ti)
        {
            for (int i = 0; i < lstTradeItems.Count; i++)
            {
                if (lstTradeItems[i].Item == ti.Item && lstTradeItems[i].Customer == ti.Customer &&
                    lstTradeItems[i].Price == ti.Price && lstTradeItems[i].StashPosition == ti.StashPosition
                    && lstTradeItems[i].TradeType == ti.TradeType)
                {
                    lstTradeItems.RemoveAt(i);
                }
            }
        }
    }

    [Serializable]
    internal class NoCurrencyBitmapFoundException : Exception
    {
        public NoCurrencyBitmapFoundException()
        {
        }

        public NoCurrencyBitmapFoundException(string message) : base(message)
        {
        }

        public NoCurrencyBitmapFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoCurrencyBitmapFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class NoCurrencyFoundException : Exception
    {
        public NoCurrencyFoundException()
        {
        }

        public NoCurrencyFoundException(string message) : base(message)
        {
        }

        public NoCurrencyFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoCurrencyFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class NoRegExMatchException : Exception
    {
        public NoRegExMatchException()
        {
        }

        public NoRegExMatchException(string message) : base(message)
        {
        }

        public NoRegExMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRegExMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class TradeItemExistsException : Exception
    {
        public TradeItemExistsException()
        {
        }

        public TradeItemExistsException(string message) : base(message)
        {
        }

        public TradeItemExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TradeItemExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
