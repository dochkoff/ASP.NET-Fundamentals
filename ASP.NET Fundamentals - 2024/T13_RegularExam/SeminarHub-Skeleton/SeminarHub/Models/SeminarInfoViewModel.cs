using SeminarHub.Data;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Models
{
    public class SeminarInfoViewModel
    {
        public SeminarInfoViewModel(
            int id,
            string topic,
            string lecturer,
            DateTime dateAndTime,
            string category,
            string organizer
            )
        {
            Id = id;
            Topic = topic;
            Lecturer = lecturer;
            DateAndTime = dateAndTime.ToString(DateFormat);
            Category = category;
            Organizer = organizer;
        }
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Lecturer { get; set; }

        public string Category { get; set; }

        public string DateAndTime { get; set; }

        public string Organizer { get; set; }
    }
}
