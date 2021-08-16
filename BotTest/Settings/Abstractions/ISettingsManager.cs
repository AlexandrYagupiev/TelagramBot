namespace Telegram.BotSettings.Abstractions
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Количество различных типов настроек в буфере
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Устанавливает настройки в буфер
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        void SetSettings<T>(T settings) where T : ISettings;

        /// <summary>
        /// Получает настройки из буфера
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetSettings<T>() where T : ISettings;
        
        /// <summary>
        /// Загружает буфер настроек из хранилища
        /// </summary>
        void LoadSettings();

        /// <summary>
        /// Сохраняет буфер настроек в хранилище
        /// </summary>
        void SaveSettings();
    }
}
