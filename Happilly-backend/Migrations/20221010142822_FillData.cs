using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Happilly_backend.Migrations
{
    public partial class FillData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Medicine",
            columns: new[] { "Name", "Description"},
            values: new object[] { "Paracetamol", "Paracetamol, also known as acetaminophen, is a medication used to treat fever and mild to moderate pain." });

            migrationBuilder.InsertData(
            table: "Medicine",
            columns: new[] { "Name", "Description" },
            values: new object[] { "Adderall", "Adderall and Mydayis are trade names for a combination drug called mixed amphetamine salts containing four salts of amphetamine." });

            migrationBuilder.InsertData(
            table: "Medicine",
            columns: new[] { "Name", "Description" },
            values: new object[] { "Ibuprofen", "Ibuprofen is a nonsteroidal anti-inflammatory drug that is used for treating pain, fever, and inflammation. This includes painful menstrual periods, migraines, and rheumatoid arthritis." });

            migrationBuilder.InsertData(
            table: "Medicine",
            columns: new[] { "Name", "Description" },
            values: new object[] { "Bisacodyl", "Bisacodyl is a laxative. This type of medicine can help you empty your bowels if you have constipation (difficulty pooing)." });

            migrationBuilder.InsertData(
            table: "Medicine",
            columns: new[] { "Name", "Description" },
            values: new object[] { "Diazepam", "Diazepam belongs to a group of medicines called benzodiazepines. Its used to treat anxiety, muscle spasms and seizures or fits. Its also used in hospital to reduce alcohol withdrawal symptoms, such as sweating or difficulty sleeping." });

            migrationBuilder.InsertData(
            table: "Medicine",
            columns: new[] { "Name", "Description" },
            values: new object[] { "Metoclopramide", "Metoclopramide is an anti-sickness medicine (known as an antiemetic). Its used to help stop you feeling or being sick (nausea or vomiting)" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
