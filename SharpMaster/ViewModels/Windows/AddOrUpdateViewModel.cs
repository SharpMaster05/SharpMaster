using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SharpMaster.ViewModels.Windows;

internal class AddOrUpdateViewModel : BaseViewModel<PersonDTO>
{
    private readonly Animation _animation;
    private readonly PersonService _personService;
    private readonly BuildService _buildService;
    private bool _isUpdate;
    public AddOrUpdateViewModel(PersonService personService, BuildService buildService)
    {
        _personService = personService;
        _buildService = buildService;
        _animation = new Animation();
        Person = new();
        BuildingsInitialize();
        WindowTitle = "Add new Person";
    }

    public AddOrUpdateViewModel(PersonService personService, BuildService buildService, PersonDTO person = null, bool isUpdate = false) : this(personService, buildService)
    {
        if (isUpdate != false)
        {
            Person = person;
            PersonImage.Source = new BitmapImage(new Uri(Person.ImagePath));
            _isUpdate = isUpdate;
            WindowTitle = $"Editing a person's {person.Name} {person.Lastname}";
        }
    }

    private async void BuildingsInitialize()
    {
        var buildings = (await _buildService.GetAllAsync()).Select(x => x.Title);
        Buildings = new(buildings);
    }

    private async void AddOrUpdatePerson()
    {
        var buildId = (await _buildService.GetAllAsync()).FirstOrDefault(x => x.Title == SelectedBuild).BuildId;
        Person.BuildId = buildId;

        if (_isUpdate)
            await _personService.UpdateAsync(Person);
        else
            await _personService.AddAsync(Person);
    }

    public PersonDTO Person { get; set; }
    public Image PersonImage { get; set; } = new Image();
    public string SelectedBuild { get; set; }
    public List<string> Buildings { get; set; }
    public string WindowTitle {  get; set; }

    public ICommand CloseWindowCommand => new Command(x =>
    {
        var border = x as Border;
        _animation.CloseAnimation(border, "AddOrUpdateView");
    });

    public ICommand SavePersonCommand => new Command(x => 
    {
        var border = x as Border;
        AddOrUpdatePerson();
        CloseWindowCommand.Execute(border);
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
                PersonImage.Source = new BitmapImage(new Uri(file.FileName));
            }
        }
    });
}
