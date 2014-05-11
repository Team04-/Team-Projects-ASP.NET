var tour;

tour = new Shepherd.Tour({
    defaults: {
        classes: 'shepherd-element shepherd-open shepherd-theme-arrows',
        scrollTo: true
    }
});



tour.addStep('Step2', {
    title: 'Select Module',
    text: 'Please select a module you would like to create a booking for.',
    attachTo: '.col-md-10b',
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
    title: 'Start day',
    text: 'What day would you like to make a booking?',
    attachTo: '.col-md-10c',
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
    title: 'Start time',
    text: 'Select the time you would like to book the room for.',
    attachTo: '.col-md-10d',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step5', {
    title: 'Start Duration',
    text: 'Now choose how long you would like to book the room for.',
    attachTo: '.col-md-10e',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step6', {
    title: 'Weeks',
    text: 'Select the weeks you would like the rooms for. If you would like them for all 15 weeks please click on the "Select All" button.',
    attachTo: '.col-md-10f',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step6', {
    title: 'Number of students',
    text: 'Now enter the number of students.',
    attachTo: '.col-md-10g',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step7', {
    title: 'Park preference',
    text: 'Now select your park preference.',
    attachTo: '.col-md-10h',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step8', {
    title: 'Number of rooms',
    text: 'Select the number of rooms you wish to book.',
    attachTo: '.col-md-10i',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step9', {
    title: 'Preferred Park',
    text: 'For your individual room preference, please select a park.',
    attachTo: '.col-md-10j',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step10', {
    title: 'Preferred Building',
    text: 'For your individual room preference, please select a building.',
    attachTo: '.col-md-10k',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step11', {
    title: 'Room type',
    text: 'Now select the type of room you would like to book.',
    attachTo: '.col-md-10l',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step12', {
    title: 'Facilities',
    text: 'Now choose your desired facilities.',
    attachTo: '#facTable',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step13', {
    title: 'Room',
    text: 'Select your preferred room.',
    attachTo: '.col-md-10m',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step14', {
    title: 'Add or delete room preference',
    text: 'Click on the appropriate button to add or delete room preference.',
    attachTo: '.roompre',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step15', {
    title: 'Priority',
    text: 'Tick the box if your room is a priority booking.',
    attachTo: '.col-md-10n',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

tour.addStep('Step15', {
    title: 'Additional Comments',
    text: 'Please Enter any additional needs you require for the room.',
    attachTo: '.col-md-10o',
    classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',
    buttons: [
      {
          text: 'Next',
          action: tour.next,
          classes: 'shepherd-button-example-primary'
      }
    ]
});

