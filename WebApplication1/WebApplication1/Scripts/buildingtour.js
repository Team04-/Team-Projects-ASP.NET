var tour;

tour = new Shepherd.Tour({
    defaults: {
        classes: 'shepherd-element shepherd-open shepherd-theme-arrows',
        scrollTo: true
    }
});

tour.addStep('add-request-start', {
    title: 'Create a new building',
    text: 'TEST',
    attachTo: '.helloworld',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

