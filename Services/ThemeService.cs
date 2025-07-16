using MudBlazor;

namespace WSService.Services
{
    public class ThemeService
    {
        private bool _isDarkMode = false;
        
        public bool IsDarkMode { get; private set; } 
         

        public MudTheme CurrentTheme 
        {
            get
            {
                return _isDarkMode ?  DarkTheme : LightTheme;
            }
        }

        public void  ToggleTheme() 
        {
            
            _isDarkMode = !_isDarkMode;
            
        } 

        public MudTheme LightTheme { get; } = new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = "#1976d2",
                Secondary = "#ff9800",
                AppbarBackground = "#1565c0",
                AppbarText = "#ffffff",
                DrawerBackground = "#e3f2fd",
                DrawerText = "#0d47a1",
                Background = "#f5f5f5",
                Surface = "#ffffff"
            }
        };

        public MudTheme DarkTheme { get; } = new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary = Colors.Blue.Lighten1,
                Secondary = Colors.Orange.Lighten2,
                Background = "#121212",
                Surface = "#1e1e1e",
                AppbarBackground = "#1f1f1f",
                AppbarText = Colors.Shades.White,
                DrawerBackground = "#1e1e1e",
                DrawerText = Colors.Shades.White,
                TextPrimary = Colors.Shades.White
            }
        }; 
    }
}