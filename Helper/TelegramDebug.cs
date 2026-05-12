namespace BBU_SYSTEM.Helper;
    public class Telegram{
        public static async Task SendDebugToMyTelegramDirect(string text)
        {
            var debugBotToken = "7392543584:AAHRwovcOkkMahXUTm6_YGg_6D-Wla6LukM";
            var chatId = 5063733056;
        
            using var http = new HttpClient();
        
            var url = $"https://api.telegram.org/bot{debugBotToken}/sendMessage";
        
            await http.PostAsJsonAsync(url, new
            {
                chat_id = chatId,
                text = text
            });
        }
}