var tour;

tour = new Shepherd.Tour({
    defaults: {
        classes: 'shepherd-element shepherd-open shepherd-theme-arrows',
        scrollTo: true
    }
});

tour.addStep('Step1', {
    title: 'Create a new module',
    text: 'Create module is used to add a module on to the database.',
    attachTo: '.tour1',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step2', {
    title: 'Edit, View details and Delete',
    text: 'We can edit, delete and view more details about each module.',
    attachTo: '.tour2',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});