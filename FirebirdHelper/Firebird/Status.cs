using System.Runtime.Serialization;
namespace DAO
{
    /// <summary>
    /// статус загрузки документа
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// выгружен
        /// </summary>
        UnLoadet = 1,
        /// <summary>
        /// ошибка выгрузки
        /// </summary>
        UnLoadetError = 2,
        /// <summary>
        /// передан
        /// </summary>
        Passed = 3,
        /// <summary>
        /// ошибка передачи
        /// </summary>
        PassedError = 4,
        /// <summary>
        /// загружен
        /// </summary>
        Loadet = 5,
        /// <summary>
        /// ошибка загрузки
        /// </summary>
        LoadetError = 6,
        /// <summary>
        /// не определено
        /// </summary>
        None = 0,
        /// <summary>
        /// предыдущая версия не найдена
        /// </summary>
        PrevNotFound = 10,
        /// <summary>
        /// найдена более поздняя загруженная версия
        /// </summary>
        LastFound = 11,
        /// <summary>
        /// загружен на удаленной машине
        /// </summary>
        RemoteLoadet = 12,
        /// <summary>
        /// отправлен по сети, но нет информации о загрузке на удаленной машине
        /// </summary>
        SendFromNet = 14,
        /// <summary>
        /// пошел процесс выгрузки по сети
        /// </summary>
        RunUnload = 15,
        /// <summary>
        /// документ в процессе загрузки по сети
        /// </summary>
        DocumentNetLoading = 16

    }
    public static class StatusHelper
    {
        public static string StatusToString(Status status)
        {
            switch (status)
            {
                case Status.Loadet:
                    return "Загружен";
                case Status.LoadetError:
                    return "Ошибка загрузки";
                case Status.None:
                    return "Не выгружался";
                case Status.Passed:
                    return "Передан";
                case Status.PassedError:
                    return "Ошибка передачи";
                case Status.PrevNotFound:
                    return "Предыдущая версия не найдена";
                case Status.UnLoadet:
                    return "Выгружен";
                case Status.UnLoadetError:
                    return "Ошибка выгрузки";
                case Status.LastFound:
                    return "Уже загружена более поздняя версия";
                case Status.RemoteLoadet:
                        return "Загружено в ДО";
                case Status.SendFromNet:
                    return "Передан на удаленный узел";
                case Status.RunUnload:
                    return "Передается";
                case Status.DocumentNetLoading:
                    return "Документ в процессе загрузки по сети";
                default:
                    throw new System.InvalidCastException();
            }
        }
    }
}