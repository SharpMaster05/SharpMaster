using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SharpMaster.ViewModels.Windows
{
    internal class EditPersonViewModel : BaseViewModel<PersonDTO>
    {
        private readonly PersonService _personService;
        private readonly BuildService _buildService;
        private readonly Animation _animation;

        public Image PersonImage { get; set; } = new Image();
        public PersonDTO Person { get; set; }
        public List<string> BuildsName { get; set; }
        public string SelectedBuildName { get; set; }
        public EditPersonViewModel(PersonService personService, BuildService buildService, PersonDTO person)
        {
            _personService = personService;
            _buildService = buildService;
            _animation = new Animation();
            Person = person;

            PersonImage.Source = new BitmapImage(new Uri(Person.ImagePath));

            Initialize();
        }

        private async void Initialize()
        {
            SelectedBuildName = (await _buildService.GetAllAsync()).FirstOrDefault(x => x.BuildId == Person.BuildId).Title;

            var builds = await _buildService.GetAllAsync();
            BuildsName = new List<string>(builds.Select(x => x.Title));
        }

        public ICommand CloseWindowCommand => new Command(x =>
        {
            try
            {
                if (x is Border border)
                    _animation.CloseAnimation(border, "EditPersonView");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand SaveCommad => new Command(async x =>
        {
            if(x is Border border)
            {
                var build = (await _buildService.GetAllAsync()).FirstOrDefault(x => x.Title == SelectedBuildName);
                Person.BuildId = build.BuildId;
                await _personService.UpdateAsync(Person);

                CloseWindowCommand.Execute(border);
            }
        },x => !string.IsNullOrEmpty(Person.Name) && 
               !string.IsNullOrEmpty(Person.Lastname) &&
               !string.IsNullOrEmpty(Person.Email) && 
               !string.IsNullOrEmpty(Person.PhoneNumber));

        public ICommand SelectImageCommand => new Command(x =>
        {
            using (OpenFileDialog file = new())
            {
                file.Filter = "Jpg files (*.jpg)|*.jpg|" +
                              "Png files (*.png)|*.png|" +
                              "Jpeg files (*.jpeg)|*.jpeg";
                file.ShowDialog();

                if (!string.IsNullOrEmpty(file.FileName))
                {
                    Person.ImagePath = file.FileName;
                    PersonImage.Source = new BitmapImage(new Uri(file.FileName));
                }
            }
        });
    }
}
