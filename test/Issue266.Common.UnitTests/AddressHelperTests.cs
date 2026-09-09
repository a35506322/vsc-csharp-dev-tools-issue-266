namespace Issue266.Common.UnitTests;

[TestClass]
public class AddressHelperTests
{
    // Control: simple DataRow args without JSON quotes. Test Explorer should list these normally.
    [TestMethod]
    [DataRow("plain", 1, "a")]
    [DataRow("plain", 2, "b")]
    [DataRow("plain", 3, "c")]
    public void FindZipCode_簡單參數_Should正常列出(string scope, int number, string caseName)
    {
        scope.Should().Be("plain");
        number.Should().BePositive();
        caseName.Should().NotBeNullOrEmpty();
    }

    // DataRow first arg is a JSON-like string with escaped quotes — this is the Test Explorer display/run bug.
    [TestMethod]
    [DataRow("[\"\",\"all\"]", 1, 0, 0, 0, 0, "EMPTY_all")] // ["","all"]
    [DataRow("[\"788\"]", 788, 0, 0, 0, 0, "N")] // ["788"]
    [DataRow("[\"248\",\"2\",\"to\",\"3flr\"]", 248, 0, 0, 0, 2, "N_N_to_Nflr")] // ["248","2","to","3flr"]
    [DataRow("[\"1\",\"2flr\"]", 1, 0, 0, 0, 2, "N_Nflr")] // ["1","2flr"]
    [DataRow("[\"97\",\"32flr\",\"lower\"]", 97, 0, 0, 0, 32, "N_Nflr_lower")] // ["97","32flr","lower"]
    [DataRow("[\"187\",\"5flr\",\"upper\"]", 187, 0, 0, 0, 5, "N_Nflr_upper")] // ["187","5flr","upper"]
    [DataRow("[\"2\",\"of\",\"1\"]", 2, 0, 0, 1, 0, "N_of_N")] // ["2","of","1"]
    [DataRow("[\"36\",\"of\",\"1\",\"to\",\"of\",\"2\"]", 36, 0, 0, 1, 0, "N_of_N_to_of_N")] // ["36","of","1","to","of","2"]
    [DataRow(
        "[\"79\",\"of\",\"1\",\"to\",\"of\",\"10\",\"4flr\",\"upper\"]",
        79,
        0,
        0,
        1,
        4,
        "N_of_N_to_of_N_Nflr_upper"
    )] // ["79","of","1","to","of","10","4flr","upper"]
    [DataRow("[\"1\",\"of\",\"17\",\"upper\"]", 1, 0, 0, 17, 0, "N_of_N_upper")] // ["1","of","17","upper"]
    [DataRow("[\"43\",\"to\",\"43\",\"of\",\"1\"]", 43, 0, 0, 1, 0, "N_to_N_of_N")] // ["43","to","43","of","1"]
    [DataRow("[\"6aly\",\"all\"]", 1, 0, 6, 0, 0, "Naly_all")] // ["6aly","all"]
    [DataRow("[\"4aly\",\"even\",\"100\",\"lower\"]", 100, 0, 4, 0, 0, "Naly_even_N_lower")] // ["4aly","even","100","lower"]
    [DataRow("[\"4aly\",\"even\",\"102\",\"upper\"]", 102, 0, 4, 0, 0, "Naly_even_N_upper")] // ["4aly","even","102","upper"]
    [DataRow("[\"4aly\",\"odd\",\"35\",\"lower\"]", 35, 0, 4, 0, 0, "Naly_odd_N_lower")] // ["4aly","odd","35","lower"]
    [DataRow(
        "[\"4aly\",\"odd\",\"35\",\"of\",\"1\",\"upper\"]",
        35,
        0,
        4,
        1,
        0,
        "Naly_odd_N_of_N_upper"
    )] // ["4aly","odd","35","of","1","upper"]
    [DataRow("[\"17ln\",\"1\"]", 1, 17, 0, 0, 0, "Nln_N")] // ["17ln","1"]
    [DataRow("[\"173ln\",\"11\",\"of\",\"8\"]", 11, 173, 0, 8, 0, "Nln_N_of_N")] // ["173ln","11","of","8"]
    [DataRow("[\"81ln\",\"9aly\",\"8\"]", 8, 81, 9, 0, 0, "Nln_Naly_N")] // ["81ln","9aly","8"]
    [DataRow("[\"160ln\",\"19aly\",\"all\"]", 1, 160, 19, 0, 0, "Nln_Naly_all")] // ["160ln","19aly","all"]
    [DataRow(
        "[\"　1250ln\",\"158aly\",\"c\",\"16\",\"lower\"]",
        16,
        1250,
        158,
        0,
        0,
        "Nln_Naly_c_N_lower"
    )] // ["　1250ln","158aly","c","16","lower"]
    [DataRow(
        "[\"722ln\",\"103aly\",\"c\",\"141\",\"upper\"]",
        141,
        722,
        103,
        0,
        0,
        "Nln_Naly_c_N_upper"
    )] // ["722ln","103aly","c","141","upper"]
    [DataRow(
        "[\"39ln\",\"50aly\",\"even\",\"68\",\"lower\"]",
        68,
        39,
        50,
        0,
        0,
        "Nln_Naly_even_N_lower"
    )] // ["39ln","50aly","even","68","lower"]
    [DataRow(
        "[\"　1250ln\",\"158aly\",\"even\",\"18\",\"to\",\"66\"]",
        18,
        1250,
        158,
        0,
        0,
        "Nln_Naly_even_N_to_N"
    )] // ["　1250ln","158aly","even","18","to","66"]
    [DataRow(
        "[\"422ln\",\"82aly\",\"even\",\"16\",\"upper\"]",
        16,
        422,
        82,
        0,
        0,
        "Nln_Naly_even_N_upper"
    )] // ["422ln","82aly","even","16","upper"]
    [DataRow("[\"96ln\",\"17aly\",\"even\",\"all\"]", 2, 96, 17, 0, 0, "Nln_Naly_even_all")] // ["96ln","17aly","even","all"]
    [DataRow(
        "[\"　1250ln\",\"130aly\",\"odd\",\"11\",\"lower\"]",
        11,
        1250,
        130,
        0,
        0,
        "Nln_Naly_odd_N_lower"
    )] // ["　1250ln","130aly","odd","11","lower"]
    [DataRow(
        "[\"　1250ln\",\"118aly\",\"odd\",\"17\",\"to\",\"45\"]",
        17,
        1250,
        118,
        0,
        0,
        "Nln_Naly_odd_N_to_N"
    )] // ["　1250ln","118aly","odd","17","to","45"]
    [DataRow(
        "[\"506ln\",\"128aly\",\"odd\",\"135\",\"upper\"]",
        135,
        506,
        128,
        0,
        0,
        "Nln_Naly_odd_N_upper"
    )] // ["506ln","128aly","odd","135","upper"]
    [DataRow("[\"96ln\",\"17aly\",\"odd\",\"all\"]", 1, 96, 17, 0, 0, "Nln_Naly_odd_all")] // ["96ln","17aly","odd","all"]
    [DataRow("[\"908ln\",\"all\"]", 1, 908, 0, 0, 0, "Nln_all")] // ["908ln","all"]
    [DataRow("[\"252ln\",\"c\",\"70\",\"lower\"]", 70, 252, 0, 0, 0, "Nln_c_N_lower")] // ["252ln","c","70","lower"]
    [DataRow(
        "[\"260ln\",\"c\",\"1\",\"of\",\"10\",\"lower\"]",
        1,
        260,
        0,
        10,
        0,
        "Nln_c_N_of_N_lower"
    )] // ["260ln","c","1","of","10","lower"]
    [DataRow(
        "[\"24ln\",\"c\",\"1\",\"of\",\"4\",\"to\",\"of\",\"8\"]",
        1,
        24,
        0,
        4,
        0,
        "Nln_c_N_of_N_to_of_N"
    )] // ["24ln","c","1","of","4","to","of","8"]
    [DataRow(
        "[\"173ln\",\"c\",\"11\",\"of\",\"9\",\"upper\"]",
        11,
        173,
        0,
        9,
        0,
        "Nln_c_N_of_N_upper"
    )] // ["173ln","c","11","of","9","upper"]
    [DataRow("[\"59ln\",\"c\",\"3\",\"to\",\"17\"]", 3, 59, 0, 0, 0, "Nln_c_N_to_N")] // ["59ln","c","3","to","17"]
    [DataRow(
        "[\"382ln\",\"c\",\"2\",\"to\",\"2\",\"of\",\"1\"]",
        2,
        382,
        0,
        1,
        0,
        "Nln_c_N_to_N_of_N"
    )] // ["382ln","c","2","to","2","of","1"]
    [DataRow("[\"220ln\",\"c\",\"9\",\"upper\"]", 9, 220, 0, 0, 0, "Nln_c_N_upper")] // ["220ln","c","9","upper"]
    [DataRow("[\"672ln\",\"c\",\"2aly\",\"lower\"]", 1, 672, 2, 0, 0, "Nln_c_Naly_lower")] // ["672ln","c","2aly","lower"]
    [DataRow("[\"330ln\",\"c\",\"56aly\",\"to\",\"260\"]", 1, 330, 56, 0, 0, "Nln_c_Naly_to_N")] // ["330ln","c","56aly","to","260"]
    [DataRow("[\"113ln\",\"c\",\"6aly\",\"to\",\"7aly\"]", 1, 113, 6, 0, 0, "Nln_c_Naly_to_Naly")] // ["113ln","c","6aly","to","7aly"]
    [DataRow("[\"672ln\",\"c\",\"3aly\",\"upper\"]", 1, 672, 3, 0, 0, "Nln_c_Naly_upper")] // ["672ln","c","3aly","upper"]
    [DataRow("[\"238ln\",\"even\",\"90\",\"lower\"]", 90, 238, 0, 0, 0, "Nln_even_N_lower")] // ["238ln","even","90","lower"]
    [DataRow(
        "[\"118ln\",\"even\",\"50\",\"of\",\"3\",\"lower\"]",
        50,
        118,
        0,
        3,
        0,
        "Nln_even_N_of_N_lower"
    )] // ["118ln","even","50","of","3","lower"]
    [DataRow(
        "[\"25ln\",\"even\",\"18\",\"of\",\"4\",\"upper\"]",
        18,
        25,
        0,
        4,
        0,
        "Nln_even_N_of_N_upper"
    )] // ["25ln","even","18","of","4","upper"]
    [DataRow(
        "[\"　1250ln\",\"even\",\"106\",\"to\",\"116\"]",
        106,
        1250,
        0,
        0,
        0,
        "Nln_even_N_to_N"
    )] // ["　1250ln","even","106","to","116"]
    [DataRow(
        "[\"53ln\",\"even\",\"24\",\"to\",\"30\",\"of\",\"2\"]",
        24,
        53,
        0,
        2,
        0,
        "Nln_even_N_to_N_of_N"
    )] // ["53ln","even","24","to","30","of","2"]
    [DataRow(
        "[\"150ln\",\"even\",\"14\",\"to\",\"20aly\"]",
        14,
        150,
        20,
        0,
        0,
        "Nln_even_N_to_Naly"
    )] // ["150ln","even","14","to","20aly"]
    [DataRow("[\"93ln\",\"even\",\"14\",\"upper\"]", 14, 93, 0, 0, 0, "Nln_even_N_upper")] // ["93ln","even","14","upper"]
    [DataRow("[\"416ln\",\"even\",\"66aly\",\"lower\"]", 2, 416, 66, 0, 0, "Nln_even_Naly_lower")] // ["416ln","even","66aly","lower"]
    [DataRow(
        "[\"423ln\",\"even\",\"4aly\",\"to\",\"6aly\"]",
        2,
        423,
        4,
        0,
        0,
        "Nln_even_Naly_to_Naly"
    )] // ["423ln","even","4aly","to","6aly"]
    [DataRow("[\"118ln\",\"even\",\"8aly\",\"upper\"]", 2, 118, 8, 0, 0, "Nln_even_Naly_upper")] // ["118ln","even","8aly","upper"]
    [DataRow("[\"960ln\",\"even\",\"all\"]", 2, 960, 0, 0, 0, "Nln_even_all")] // ["960ln","even","all"]
    [DataRow("[\"67ln\",\"odd\",\"23\",\"lower\"]", 23, 67, 0, 0, 0, "Nln_odd_N_lower")] // ["67ln","odd","23","lower"]
    [DataRow(
        "[\"23ln\",\"odd\",\"5\",\"of\",\"2\",\"lower\"]",
        5,
        23,
        0,
        2,
        0,
        "Nln_odd_N_of_N_lower"
    )] // ["23ln","odd","5","of","2","lower"]
    [DataRow("[\"506ln\",\"odd\",\"67\",\"to\",\"169\"]", 67, 506, 0, 0, 0, "Nln_odd_N_to_N")] // ["506ln","odd","67","to","169"]
    [DataRow(
        "[\"150ln\",\"odd\",\"379\",\"to\",\"431aly\"]",
        379,
        150,
        431,
        0,
        0,
        "Nln_odd_N_to_Naly"
    )] // ["150ln","odd","379","to","431aly"]
    [DataRow("[\"265ln\",\"odd\",\"19\",\"upper\"]", 19, 265, 0, 0, 0, "Nln_odd_N_upper")] // ["265ln","odd","19","upper"]
    [DataRow("[\"126ln\",\"odd\",\"91aly\",\"lower\"]", 1, 126, 91, 0, 0, "Nln_odd_Naly_lower")] // ["126ln","odd","91aly","lower"]
    [DataRow("[\"12ln\",\"odd\",\"5aly\",\"to\",\"7aly\"]", 1, 12, 5, 0, 0, "Nln_odd_Naly_to_Naly")] // ["12ln","odd","5aly","to","7aly"]
    [DataRow("[\"680ln\",\"odd\",\"315aly\",\"upper\"]", 1, 680, 315, 0, 0, "Nln_odd_Naly_upper")] // ["680ln","odd","315aly","upper"]
    [DataRow("[\"439ln\",\"odd\",\"all\"]", 1, 439, 0, 0, 0, "Nln_odd_all")] // ["439ln","odd","all"]
    [DataRow("[\"all\"]", 1, 0, 0, 0, 0, "all")] // ["all"]
    [DataRow("[\"c\",\"376\",\"lower\"]", 376, 0, 0, 0, 0, "c_N_lower")] // ["c","376","lower"]
    [DataRow("[\"c\",\"82\",\"of\",\"3\",\"lower\"]", 82, 0, 0, 3, 0, "c_N_of_N_lower")] // ["c","82","of","3","lower"]
    [DataRow("[\"c\",\"196\",\"of\",\"2\",\"to\",\"499\"]", 196, 0, 0, 2, 0, "c_N_of_N_to_N")] // ["c","196","of","2","to","499"]
    [DataRow(
        "[\"c\",\"63\",\"of\",\"2\",\"to\",\"95\",\"of\",\"10\"]",
        63,
        0,
        0,
        2,
        0,
        "c_N_of_N_to_N_of_N"
    )] // ["c","63","of","2","to","95","of","10"]
    [DataRow("[\"c\",\"9\",\"of\",\"8\",\"upper\"]", 9, 0, 0, 8, 0, "c_N_of_N_upper")] // ["c","9","of","8","upper"]
    [DataRow("[\"c\",\"550\",\"to\",\"700\"]", 550, 0, 0, 0, 0, "c_N_to_N")] // ["c","550","to","700"]
    [DataRow(
        "[\"c\",\"212\",\"to\",\"220\",\"4flr\",\"upper\"]",
        212,
        0,
        0,
        0,
        4,
        "c_N_to_N_Nflr_upper"
    )] // ["c","212","to","220","4flr","upper"]
    [DataRow("[\"c\",\"50\",\"to\",\"65\",\"all\"]", 50, 0, 0, 0, 0, "c_N_to_N_all")] // ["c","50","to","65","all"]
    [DataRow("[\"c\",\"109\",\"to\",\"210\",\"of\",\"1\"]", 109, 0, 0, 1, 0, "c_N_to_N_of_N")] // ["c","109","to","210","of","1"]
    [DataRow("[\"c\",\"68\",\"to\",\"136ln\"]", 68, 136, 0, 0, 0, "c_N_to_Nln")] // ["c","68","to","136ln"]
    [DataRow("[\"c\",\"561\",\"upper\"]", 561, 0, 0, 0, 0, "c_N_upper")] // ["c","561","upper"]
    [DataRow(
        "[\"c\",\"256aly\",\"to\",\"399\",\"of\",\"13\"]",
        1,
        0,
        256,
        13,
        0,
        "c_Naly_to_N_of_N"
    )] // ["c","256aly","to","399","of","13"]
    [DataRow("[\"c\",\"25ln\",\"lower\"]", 1, 25, 0, 0, 0, "c_Nln_lower")] // ["c","25ln","lower"]
    [DataRow("[\"c\",\"2ln\",\"to\",\"599\"]", 1, 2, 0, 0, 0, "c_Nln_to_N")] // ["c","2ln","to","599"]
    [DataRow("[\"c\",\"2ln\",\"to\",\"51\",\"of\",\"4\"]", 1, 2, 0, 4, 0, "c_Nln_to_N_of_N")] // ["c","2ln","to","51","of","4"]
    [DataRow("[\"c\",\"175ln\",\"to\",\"176ln\"]", 1, 175, 0, 0, 0, "c_Nln_to_Nln")] // ["c","175ln","to","176ln"]
    [DataRow("[\"c\",\"78ln\",\"upper\"]", 1, 78, 0, 0, 0, "c_Nln_upper")] // ["c","78ln","upper"]
    [DataRow("[\"even\",\"712\",\"lower\"]", 712, 0, 0, 0, 0, "even_N_lower")] // ["even","712","lower"]
    [DataRow("[\"even\",\"72\",\"of\",\"3\",\"lower\"]", 72, 0, 0, 3, 0, "even_N_of_N_lower")] // ["even","72","of","3","lower"]
    [DataRow("[\"even\",\"12\",\"of\",\"8\",\"to\",\"38\"]", 12, 0, 0, 8, 0, "even_N_of_N_to_N")] // ["even","12","of","8","to","38"]
    [DataRow(
        "[\"even\",\"148\",\"of\",\"1\",\"to\",\"498\",\"all\"]",
        148,
        0,
        0,
        1,
        0,
        "even_N_of_N_to_N_all"
    )] // ["even","148","of","1","to","498","all"]
    [DataRow(
        "[\"even\",\"14\",\"of\",\"2\",\"to\",\"14\",\"of\",\"6\"]",
        14,
        0,
        0,
        2,
        0,
        "even_N_of_N_to_N_of_N"
    )] // ["even","14","of","2","to","14","of","6"]
    [DataRow(
        "[\"even\",\"666\",\"of\",\"1\",\"to\",\"700ln\"]",
        666,
        700,
        0,
        1,
        0,
        "even_N_of_N_to_Nln"
    )] // ["even","666","of","1","to","700ln"]
    [DataRow("[\"even\",\"56\",\"of\",\"1\",\"upper\"]", 56, 0, 0, 1, 0, "even_N_of_N_upper")] // ["even","56","of","1","upper"]
    [DataRow("[\"even\",\"90\",\"to\",\"150\"]", 90, 0, 0, 0, 0, "even_N_to_N")] // ["even","90","to","150"]
    [DataRow("[\"even\",\"150\",\"to\",\"150\",\"all\"]", 150, 0, 0, 0, 0, "even_N_to_N_all")] // ["even","150","to","150","all"]
    [DataRow("[\"even\",\"222\",\"to\",\"292\",\"of\",\"3\"]", 222, 0, 0, 3, 0, "even_N_to_N_of_N")] // ["even","222","to","292","of","3"]
    [DataRow("[\"even\",\"54\",\"to\",\"60ln\"]", 54, 60, 0, 0, 0, "even_N_to_Nln")] // ["even","54","to","60ln"]
    [DataRow("[\"even\",\"754\",\"upper\"]", 754, 0, 0, 0, 0, "even_N_upper")] // ["even","754","upper"]
    [DataRow("[\"even\",\"144ln\",\"lower\"]", 2, 144, 0, 0, 0, "even_Nln_lower")] // ["even","144ln","lower"]
    [DataRow("[\"even\",\"2ln\",\"to\",\"134\"]", 2, 2, 0, 0, 0, "even_Nln_to_N")] // ["even","2ln","to","134"]
    [DataRow("[\"even\",\"68ln\",\"to\",\"70\",\"all\"]", 2, 68, 0, 0, 0, "even_Nln_to_N_all")] // ["even","68ln","to","70","all"]
    [DataRow(
        "[\"even\",\"30ln\",\"to\",\"60\",\"of\",\"1\"]",
        2,
        30,
        0,
        1,
        0,
        "even_Nln_to_N_of_N"
    )] // ["even","30ln","to","60","of","1"]
    [DataRow("[\"even\",\"172ln\",\"to\",\"176ln\"]", 2, 172, 0, 0, 0, "even_Nln_to_Nln")] // ["even","172ln","to","176ln"]
    [DataRow("[\"even\",\"400ln\",\"upper\"]", 2, 400, 0, 0, 0, "even_Nln_upper")] // ["even","400ln","upper"]
    [DataRow("[\"even\",\"all\"]", 2, 0, 0, 0, 0, "even_all")] // ["even","all"]
    [DataRow("[\"odd\",\"625\",\"lower\"]", 625, 0, 0, 0, 0, "odd_N_lower")] // ["odd","625","lower"]
    [DataRow(
        "[\"odd\",\"3\",\"lower\",\"2flr\",\"upper\"]",
        3,
        0,
        0,
        0,
        2,
        "odd_N_lower_Nflr_upper"
    )] // ["odd","3","lower","2flr","upper"]
    [DataRow("[\"odd\",\"25\",\"of\",\"1\",\"lower\"]", 25, 0, 0, 1, 0, "odd_N_of_N_lower")] // ["odd","25","of","1","lower"]
    [DataRow("[\"odd\",\"41\",\"of\",\"3\",\"to\",\"87\"]", 41, 0, 0, 3, 0, "odd_N_of_N_to_N")] // ["odd","41","of","3","to","87"]
    [DataRow(
        "[\"odd\",\"141\",\"of\",\"1\",\"to\",\"143\",\"of\",\"8\"]",
        141,
        0,
        0,
        1,
        0,
        "odd_N_of_N_to_N_of_N"
    )] // ["odd","141","of","1","to","143","of","8"]
    [DataRow("[\"odd\",\"123\",\"of\",\"1\",\"upper\"]", 123, 0, 0, 1, 0, "odd_N_of_N_upper")] // ["odd","123","of","1","upper"]
    [DataRow("[\"odd\",\"181\",\"to\",\"225\"]", 181, 0, 0, 0, 0, "odd_N_to_N")] // ["odd","181","to","225"]
    [DataRow(
        "[\"odd\",\"41\",\"to\",\"47\",\"3flr\",\"upper\"]",
        41,
        0,
        0,
        0,
        3,
        "odd_N_to_N_Nflr_upper"
    )] // ["odd","41","to","47","3flr","upper"]
    [DataRow("[\"odd\",\"187\",\"to\",\"215\",\"all\"]", 187, 0, 0, 0, 0, "odd_N_to_N_all")] // ["odd","187","to","215","all"]
    [DataRow("[\"odd\",\"59\",\"to\",\"77\",\"of\",\"5\"]", 59, 0, 0, 5, 0, "odd_N_to_N_of_N")] // ["odd","59","to","77","of","5"]
    [DataRow("[\"odd\",\"331\",\"to\",\"391ln\"]", 331, 391, 0, 0, 0, "odd_N_to_Nln")] // ["odd","331","to","391ln"]
    [DataRow("[\"odd\",\"485\",\"upper\"]", 485, 0, 0, 0, 0, "odd_N_upper")] // ["odd","485","upper"]
    [DataRow("[\"odd\",\"53aly\",\"lower\"]", 1, 0, 53, 0, 0, "odd_Naly_lower")] // ["odd","53aly","lower"]
    [DataRow("[\"odd\",\"5aly\",\"upper\"]", 1, 0, 5, 0, 0, "odd_Naly_upper")] // ["odd","5aly","upper"]
    [DataRow("[\"odd\",\"119ln\",\"lower\"]", 1, 119, 0, 0, 0, "odd_Nln_lower")] // ["odd","119ln","lower"]
    [DataRow("[\"odd\",\"75ln\",\"to\",\"165\"]", 1, 75, 0, 0, 0, "odd_Nln_to_N")] // ["odd","75ln","to","165"]
    [DataRow("[\"odd\",\"449ln\",\"to\",\"451\",\"all\"]", 1, 449, 0, 0, 0, "odd_Nln_to_N_all")] // ["odd","449ln","to","451","all"]
    [DataRow("[\"odd\",\"69ln\",\"to\",\"71\",\"of\",\"7\"]", 1, 69, 0, 7, 0, "odd_Nln_to_N_of_N")] // ["odd","69ln","to","71","of","7"]
    [DataRow("[\"odd\",\"71ln\",\"to\",\"525ln\"]", 1, 71, 0, 0, 0, "odd_Nln_to_Nln")] // ["odd","71ln","to","525ln"]
    [DataRow("[\"odd\",\"171ln\",\"upper\"]", 1, 171, 0, 0, 0, "odd_Nln_upper")] // ["odd","171ln","upper"]
    [DataRow("[\"odd\",\"all\"]", 1, 0, 0, 0, 0, "odd_all")] // ["odd","all"]
    public void FindZipCode_符合範圍模板_Should回傳對應郵遞區號(
        string scope,
        int number,
        int lane,
        int alley,
        int subNumber,
        int floor,
        string caseName
    )
    {
        scope.Should().StartWith("[");
        caseName.Should().NotBeNullOrEmpty();
        _ = (number, lane, alley, subNumber, floor);
    }

    [TestMethod]
    [DataRow("[\"of\",\"1\",\"to\",\"of\",\"3\"]", 1, 0, 0, 1, 0, "of_N_to_of_N")] // ["of","1","to","of","3"]
    [DataRow("[\"c\",\"not-a-number\",\"lower\"]", 121, 0, 0, 0, 0, "parse_fail")]
    [DataRow("[\"12\",\"2flr\"]", 12, 0, 0, 0, 0, "floor_missing")]
    [DataRow("[\"92ln\",\"all\"]", 1, 91, 0, 0, 0, "lane_mismatch")]
    [DataRow("[\"even\",\"all\"]", 1, 0, 0, 0, 0, "even_all_with_odd_number")]
    public void FindZipCode_無法命中_Should回傳後兩碼歸零(
        string scope,
        int number,
        int lane,
        int alley,
        int subNumber,
        int floor,
        string caseName
    )
    {
        scope.Should().StartWith("[");
        caseName.Should().NotBeNullOrEmpty();
        _ = (number, lane, alley, subNumber, floor);
    }
}
