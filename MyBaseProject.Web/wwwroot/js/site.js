function successMessage(msg) {
  $.toast({
    heading: 'Success',
    text: msg,
    position: 'top-right',
    stack: false,
    icon: 'success'
  });
}


function errorMessage(msg) {
  $.toast({
    heading: 'Error',
    text: msg,
    position: 'top-right',
    stack: false,
    icon: 'error'
  });
}
