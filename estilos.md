# Clase: Angular - Estilos

## Objetivos de la clase

Al final de la clase, el estudiante debería poder:

1. Entender cómo Angular gestiona estilos globales y estilos por componente.
2. Diferenciar estilos globales, estilos encapsulados e inline styles.
3. Comprender las estrategias de encapsulación de estilos de Angular.
4. Usar CSS y SCSS dentro de un proyecto Angular.
5. Integrar Bootstrap o Angular Material en una aplicación Angular.
6. Aplicar buenas prácticas para organizar estilos en una aplicación Angular.

---

# 1. Estilos en Angular

Angular permite trabajar estilos de manera modular. Cada componente puede tener sus propios estilos, y además existe un archivo global para estilos compartidos por toda la aplicación.

La idea principal es separar responsabilidades:

- los estilos globales definen reglas generales de la aplicación;
- los estilos de componente definen la apariencia específica de un componente;
- la encapsulación evita que los estilos de un componente afecten accidentalmente a otros.

---

# 2. Formas de declarar estilos en Angular

## 2.1 Estilos globales

Los estilos globales se definen normalmente en:

```txt
src/styles.css
```

Estos estilos afectan a toda la aplicación.

Ejemplo:

```css
body {
  margin: 0;
  font-family: Arial, sans-serif;
  background-color: #f5f5f5;
}

:root {
  --primary-color: #3498db;
  --secondary-color: #2ecc71;
  --danger-color: #e74c3c;
}
```

Conviene usar estilos globales para:

- tipografías generales;
- estilos del `body`;
- variables CSS;
- resets;
- clases utilitarias;
- imports de librerías externas;
- temas generales de la aplicación.

No conviene poner todos los estilos en `styles.css`, porque se pierde modularidad y aumenta el riesgo de conflictos.

---

## 2.2 Estilos por componente

Cada componente puede tener su propio archivo CSS o SCSS.

Ejemplo:

```ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent {}
```

HTML del componente:

```html
<div class="card">
  <h2>Producto</h2>
  <p>Descripción del producto.</p>
  <button>Comprar</button>
</div>
```

CSS del componente:

```css
.card {
  border: 1px solid #ddd;
  padding: 16px;
  border-radius: 8px;
  background-color: white;
}

.card h2 {
  color: #3498db;
}

.card button {
  padding: 8px 12px;
  cursor: pointer;
}
```

Estos estilos quedan asociados al componente y, por defecto, Angular evita que afecten directamente a otros componentes.

---

## 2.3 Estilos inline en el decorador

También se pueden declarar estilos directamente en el decorador `@Component`.

```ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-example',
  template: `
    <h1>Hola Angular</h1>
  `,
  styles: [`
    h1 {
      color: blue;
    }
  `]
})
export class ExampleComponent {}
```

Este enfoque sirve para componentes muy pequeños, ejemplos o pruebas rápidas.

No conviene usarlo como regla general en proyectos reales, porque reduce la legibilidad cuando el componente crece.

---

# 3. Encapsulación de estilos en Angular

Angular permite definir cómo se aplican los estilos de un componente usando `ViewEncapsulation`.

Las estrategias principales son:

| Estrategia | Descripción | Uso recomendado |
|---|---|---|
| `Emulated` | Angular simula encapsulación agregando atributos especiales al HTML | Opción normal |
| `None` | No hay encapsulación; los estilos pasan a ser globales | Casos puntuales |
| `ShadowDom` | Usa Shadow DOM nativo del navegador | Componentes con aislamiento fuerte |

---

## 3.1 ViewEncapsulation.Emulated

Es la opción por defecto.

```ts
import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-example',
  templateUrl: './example.component.html',
  styleUrls: ['./example.component.css'],
  encapsulation: ViewEncapsulation.Emulated
})
export class ExampleComponent {}
```

Angular agrega atributos internos al HTML para que los estilos del componente solo apliquen dentro de ese componente.

Ejemplo conceptual:

```html
<app-example _nghost-ng-c123>
  <h2 _ngcontent-ng-c123>Hola</h2>
</app-example>
```

Y el CSS se transforma internamente para aplicar solo a esos elementos.

Idea para explicar en clase:

> `Emulated` no usa Shadow DOM real. Angular simula el aislamiento de estilos agregando atributos al HTML generado.

---

## 3.2 ViewEncapsulation.None

```ts
import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-global-card',
  templateUrl: './global-card.component.html',
  styleUrls: ['./global-card.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class GlobalCardComponent {}
```

Con `None`, los estilos del componente pasan a comportarse como estilos globales.

Ejemplo:

```css
h2 {
  color: red;
}
```

Ese `h2` podría afectar a otros `h2` de la aplicación.

Uso recomendado:

- casos muy puntuales;
- estilos que intencionalmente deben ser globales;
- customización de algunas librerías externas.

Advertencia:

> `ViewEncapsulation.None` puede generar conflictos visuales difíciles de detectar. No debería usarse por comodidad.

---

## 3.3 ViewEncapsulation.ShadowDom

```ts
import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-shadow-card',
  templateUrl: './shadow-card.component.html',
  styleUrls: ['./shadow-card.component.css'],
  encapsulation: ViewEncapsulation.ShadowDom
})
export class ShadowCardComponent {}
```

Con `ShadowDom`, Angular usa el Shadow DOM nativo del navegador.

Ventajas:

- aislamiento real de estilos;
- útil para componentes reutilizables;
- evita interferencias externas.

Limitaciones:

- los estilos globales no entran de la misma forma;
- puede complicar el uso de temas globales;
- no siempre es necesario para aplicaciones comunes.

---

# 4. CSS vs SCSS en Angular

Angular permite usar CSS, SCSS, Sass y Less. En la práctica, los formatos más usados son CSS y SCSS.

La diferencia importante no está en Angular como framework, sino en cómo se escriben los estilos.

- `CSS` es el lenguaje estándar que entiende el navegador.
- `SCSS` es una sintaxis de Sass que agrega utilidades como variables, anidación, mixins y archivos parciales.

## 4.1 Usar CSS en Angular

Ejemplo en un componente:

```ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent {}
```

Archivo de estilos:

```css
.card {
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 16px;
}

.card h2 {
  color: #3498db;
}

.card button {
  background-color: #3498db;
  color: white;
}
```

Ventajas de CSS:

- es más simple de aprender;
- no agrega sintaxis extra;
- es suficiente para proyectos pequeños o ejemplos de clase;
- hoy CSS moderno ya incluye variables nativas y muchas capacidades útiles.

## 4.2 Usar SCSS en Angular

Ejemplo equivalente:

```ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {}
```

Archivo de estilos:

```scss
$primary-color: #3498db;

.card {
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 16px;

  h2 {
    color: $primary-color;
  }

  button {
    background-color: $primary-color;
    color: white;
  }
}
```

Ventajas de SCSS:

- permite anidar selectores y agrupar mejor estilos relacionados;
- permite usar variables reutilizables;
- facilita crear mixins y dividir estilos en archivos parciales;
- suele escalar mejor cuando la aplicación crece.

## 4.3 Qué cambia realmente en Angular

Desde Angular, el comportamiento general es el mismo:

- los estilos pueden ser globales o por componente;
- la encapsulación funciona igual con CSS o SCSS;
- el archivo se referencia desde el decorador del componente;
- Angular procesa el archivo y lo incorpora al build.

## 4.4 Cuándo conviene usar cada uno

Conviene usar `CSS` cuando:

- el proyecto es pequeño;
- se busca simplicidad;
- el objetivo es enseñar conceptos base sin sumar sintaxis nueva;
- los estilos no tienen mucha reutilización.

Conviene usar `SCSS` cuando:

- hay muchos componentes;
- se repiten colores, tamaños o reglas de diseño;
- se quiere una organización más mantenible;
- el proyecto va a crecer.

---

# 5. Bootstrap en Angular

Bootstrap es una biblioteca CSS que permite construir interfaces rápidamente usando clases predefinidas.

## 5.1 Instalación

```bash
npm install bootstrap
```

## 5.2 Agregar Bootstrap en `angular.json`

```json
"styles": [
  "src/styles.scss",
  "node_modules/bootstrap/dist/css/bootstrap.min.css"
]
```

## 5.3 Uso

```html
<div class="container mt-4">
  <h1 class="mb-3">Listado de productos</h1>

  <button class="btn btn-primary">
    Crear producto
  </button>
</div>
```

Bootstrap es útil cuando se quiere avanzar rápido sin diseñar todos los componentes desde cero.

---

# 6. Angular Material

Angular Material es una biblioteca de componentes visuales basada en Material Design.

## 6.1 Instalación

```bash
ng add @angular/material
```

El CLI puede configurar:

- tema;
- animaciones;
- imports necesarios;
- estilos globales.

---

## 6.2 Uso con standalone components

```ts
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-example',
  standalone: true,
  imports: [MatButtonModule],
  template: `
    <button mat-raised-button color="primary">
      Guardar
    </button>
  `
})
export class ExampleComponent {}
```

Angular Material conviene cuando se quiere una UI consistente, accesible y con componentes ya resueltos.

---
