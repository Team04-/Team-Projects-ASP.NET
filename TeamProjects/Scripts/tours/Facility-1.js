var tour;

tour = new Shepherd.Tour({
    defaults: {
        classes: 'shepherd-element shepherd-open shepherd-theme-arrows',
        scrollTo: true
    }
});

tour.addStep('step1', {
    title: 'Create a new Facility',
    text: 'Here we can add a new Facility.',
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

tour.addStep('step2', {
    title: 'Add, delete or view details',
    text: 'By clicking on one of these links, we can add, delete or view details of a Facility',
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