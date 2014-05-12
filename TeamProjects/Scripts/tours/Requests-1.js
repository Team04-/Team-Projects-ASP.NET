var tour;

tour = new Shepherd.Tour({
    defaults: {
        classes: 'shepherd-element shepherd-open shepherd-theme-arrows',
        scrollTo: true
    }
});


tour.addStep('Step1', {
    title: 'Create a new request',
    text: 'Click here to create a new request.',
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
    title: 'Filter By Rounds',
    text: 'Click on the checkboxes to filter the requests by rounds.',
    attachTo: '.roundsOptions change-search',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step3', {
    title: 'Choose a module',
    text: 'Here we can select a module to view.',
    attachTo: ".moduleSelect change-search",
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step4', {
    title: 'Search Comments',
    text: 'Here you can filter your search with comments.',
    attachTo: ".commentSearch",
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});