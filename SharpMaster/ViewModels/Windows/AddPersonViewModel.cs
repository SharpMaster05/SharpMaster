using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SharpMaster.ViewModels.Windows;

internal class AddPersonViewModel : BaseViewModel<PersonDTO>
{
    private readonly PersonService _service;
    private readonly Animation _animation;
    private readonly BuildService _buildService;

    public PersonDTO Person { get; set; }
    public Image PersonImage { get; set; }
    public List<string> BuildsName { get; set; }
    public string SelectedBuildName { get; set; }
    public AddPersonViewModel(PersonService service, Animation animation, BuildService buildService)
    {
        _service = service;
        _animation = animation;
        _buildService = buildService;

        Person = new PersonDTO();
        
        InitializeBuildings();
    }
    private async void InitializeBuildings()
    {
        var builds = await _buildService.GetAllAsync();
        BuildsName = new List<string>(builds.Select(x => x.Title));
    }

    public ICommand AddPersonCommand => new Command(async x =>
    {
        if (x is Border border)
        {
            var buildId = (await _buildService.GetAllAsync()).FirstOrDefault(x => x.Title == SelectedBuildName).BuildId;

            await _service.AddAsync(new PersonDTO
            {
                Name = Person.Name,
                Lastname = Person.Lastname,
                PhoneNumber = Person.PhoneNumber,
                Email = Person.Email,
                Description = Person.Description,
                ImagePath = Person.ImagePath,
                Resume = Person.Resume,
                BuildId = buildId
            });

            CloseWindowCommand.Execute(border);
        }
    }, x => !string.IsNullOrEmpty(Person.Name) &&
            !string.IsNullOrEmpty(Person.Lastname) &&
            !string.IsNullOrEmpty(Person.Email) &&
            !string.IsNullOrEmpty(Person.PhoneNumber));

    public ICommand CloseWindowCommand => new Command(x =>
    {
        if (x is Border border)
            _animation.CloseAnimation(border, "AddPersonView");
    });

    public ICommand MinimizeWindowCommand => new Command(x =>
    {
        if (x is Window window) _animation.MinimizeAnimation(window);
    });

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
                PersonImage = new Image();
                BitmapImage img = new BitmapImage();

                img.BeginInit();
                img.UriSource = new Uri(file.FileName);
                img.EndInit();

                PersonImage.Source = img;
            }
        }
    });
}
