using System.Drawing;

namespace MobisHaims.Ui
{
    // 전 화면 공통 색상/폰트. 한글 렌더링을 위해 기본 글꼴은 "Gulim" 사용.
    public static class Theme
    {
        // 상단 헤더
        public static readonly Color HeaderBack = Color.FromArgb(44, 107, 176);
        public static readonly Color HeaderFore = Color.White;

        // 하단 푸터
        public static readonly Color FooterBack = Color.FromArgb(230, 230, 230);
        public static readonly Color FooterFore = Color.FromArgb(30, 30, 30);

        // 메인메뉴(어두운 배경 + 타일 버튼)
        public static readonly Color MainBack = Color.FromArgb(74, 85, 104);
        public static readonly Color TileBack = Color.FromArgb(52, 58, 66);
        public static readonly Color TileFore = Color.White;

        // 2차 메뉴 버튼(파란색 계열)
        public static readonly Color MenuBtnBack = Color.FromArgb(93, 158, 214);
        public static readonly Color MenuBtnFore = Color.White;

        // 업무화면(밝은 배경)
        public static readonly Color WorkBack = Color.White;
        public static readonly Color ScanBack = Color.FromArgb(180, 230, 180);   // 스캔 입력(연두)
        public static readonly Color QtyBack = Color.FromArgb(255, 250, 190);     // 수량 입력(노랑)
        public static readonly Color Accent = Color.FromArgb(200, 40, 40);        // 예약/강조(빨강)

        public static readonly Font TitleFont = new Font("Gulim", 9f, FontStyle.Bold);
        public static readonly Font BodyFont = new Font("Gulim", 9f, FontStyle.Regular);
        public static readonly Font BigFont = new Font("Gulim", 12f, FontStyle.Bold);
        public static readonly Font BtnFont = new Font("Gulim", 9f, FontStyle.Bold);
    }
}
