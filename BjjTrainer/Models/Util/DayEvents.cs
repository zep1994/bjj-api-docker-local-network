using BjjTrainer.Models.DTO.Events;
using System.Collections.ObjectModel;

namespace BjjTrainer.Models.Util
{
    public class DayEvents
    {
        public string DayHeader { get; set; }
        public ObservableCollection<CalendarEventDto> Events { get; set; } = [];
        public bool HasEvents { get; set; }
    }
}
