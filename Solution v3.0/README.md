# Version 3.0 - Custom Shape Room with Windows/Doors

- User input *room type* (Rectangular or Custom)

<table>
  <tbody>
    <tr>
      <th>Rectangular</th>
      <th>Custom</th>
    </tr>
    <tr>
      <td>
        <ul>
          <li>User input <i>width</i></li>
          <li>User input <i>height</i></li>
          <li>User input <i>depth</i></li>
        </ul>
        All of the above inputs are checked to make sure the user has entered a valid numeric value.
      </td>
      <td>
        <ul>
          <li>User input <i>coordinates</i></li>
          This input is taken as coordinates (in the form X Y) and checked against the following criteria:
          <ul>
            <li>2 values entered</li>
            <li>Values are numeric</li>
          </ul>
          <li>User input <i>height</i></li>
        </ul>
        User then enters 'F' when finished. (A check is performed to make sure the room has at least 3 corners).
      </td>
    </tr>
  </tbody>
</table>

- User input *windows/doors/areas not to be painted*

  This input is taken as dimensions(in the form WIDTH HEIGHT) and checked against the following criteria:
  
  - 2 values entered
  - Values are numeric
  - Height of individual window does not exceed room height
  - Total width of windows does not exceed room perimeter\
  
  User then enters 'F' when finished.
  
- User input *

---

- Output required results: *Floor Area*, *Paint Required* (calculated with window areas deducted), *Room Volume*.

**In the case of a custom shape room, the floor area is calculated by using the [Shoelace Algorithm](https://en.wikipedia.org/wiki/Shoelace_formula).**
