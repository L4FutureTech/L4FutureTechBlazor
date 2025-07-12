# L4FutureTechBlazor

Add in the App.razor / index.html in the header 
```html
<link rel="stylesheet" href="_content/L4FutureTech.public.L4FutureTechBlazor/css/l4fcomponentbundle.css" />
```

and in the body 
```html
<script src="_content/L4FutureTech.public.L4FutureTechBlazor/js/l4fcomponentbundle.js"></script>
```

Also in the app.cs / MauiProgram.cs 
```html
using L4FutureTechBlazor.Services; 

...

_ = builder.Services.Add_L4FutureTechBlazor_DI(builder.Configuration);
```