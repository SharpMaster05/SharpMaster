using BLL.DTO;
using BLL.Services;
using System.Collections.ObjectModel;

namespace SharpMaster.ViewModels.Pages;

internal class PersonViewModel : BaseViewModel<PersonDTO>
{
    private readonly PersonService _personService;
    public PersonViewModel(PersonService personService) : base(personService)
    {
        _personService = personService;

        Items = new(_personService.GetAll());
    }

}
