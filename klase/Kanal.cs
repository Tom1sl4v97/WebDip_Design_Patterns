namespace ttomiek_zadaca_1.klase
{
    internal class Kanal
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<idKanal>idKanal;?))(?=.*(?<frekvencija>frekvencija;?))(?=.*(?<maksimalanBroj>maksimalanBroj;?)).+";
        public int idKanal { get; set; }
        public int frekvencija { get; set; }
        public int maksimalanBroj { get; set; }
    }
}
