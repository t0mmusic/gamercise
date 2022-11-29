using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Serilog;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace Logging
{
    public class Person
    {
        // Attributes of SQLite class
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Person() { }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public partial class MainPage : ContentPage
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true)
            .Build();

        public MainPage()
        {
            InitializeComponent();

            nameEntry.Text = Directory.GetCurrentDirectory();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        /**
        * @desc     On page load, syncs app with database
        */
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        /**
        * @desc     On button clicked, adds a new Person object to the database
        *           If either field is not filled in, will not add new Object
        *           Clears both fields when complete
        * @param    {object} sender:
        * @param    {EventArgs} e:
        */
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person(nameEntry.Text, int.Parse(ageEntry.Text)));

                nameEntry.Text = ageEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }
    }

}
