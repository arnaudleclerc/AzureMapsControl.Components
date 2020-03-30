window.azureMapsControl = {

  addMap: function (divId, subscriptionKey) {
    new atlas.Map(divId, {
      authOptions: {
        authType: 'subscriptionKey',
        subscriptionKey: subscriptionKey
      }
    });
    }

};
