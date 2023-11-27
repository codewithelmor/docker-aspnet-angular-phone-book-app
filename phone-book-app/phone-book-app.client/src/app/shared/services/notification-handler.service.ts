import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class NotificationHandlerService {

  constructor() { }

  handleCreate(observable: Observable<any>, nextCallback: (data: any) => void) {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "question",
      showCancelButton: true,
      confirmButtonText: "Yes, create it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {

        observable
          .subscribe({
            next: nextCallback,
            error: (error) => {
              // console.error(error);

              swalWithBootstrapButtons.fire({
                title: "Failed!",
                text: "Creation process has failed.",
                icon: "error"
              });
            },
            complete: () => {
              // console.info('complete');

              swalWithBootstrapButtons.fire({
                title: "Created!",
                text: "Creation process was completed.",
                icon: "success"
              });
            }
          });

      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire({
          title: "Cancelled",
          text: "Creation process was cancelled.",
          icon: "error"
        });
      }
    });
  }

  handleUpdate(observable: Observable<any>, nextCallback: (data: any) => void) {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "question",
      showCancelButton: true,
      confirmButtonText: "Yes, update it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {

        observable
          .subscribe({
            next: nextCallback,
            error: (error) => {
              // console.error(error);

              swalWithBootstrapButtons.fire({
                title: "Failed!",
                text: "Update process has failed.",
                icon: "error"
              });
            },
            complete: () => {
              // console.info('complete');

              swalWithBootstrapButtons.fire({
                title: "Updated!",
                text: "Update process was completed.",
                icon: "success"
              });
            }
          });

      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire({
          title: "Cancelled",
          text: "Update process was cancelled.",
          icon: "error"
        });
      }
    });
  }  

  handleDelete(observable: Observable<any>, nextCallback: VoidFunction) {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {

        observable
          .subscribe({
            next: nextCallback,
            error: (error) => {
              // console.error(error);

              swalWithBootstrapButtons.fire({
                title: "Failed!",
                text: "Deletion process has failed.",
                icon: "error"
              });
            },
            complete: () => {
              // console.info('complete');

              swalWithBootstrapButtons.fire({
                title: "Deleted!",
                text: "Deletion process was completed.",
                icon: "success"
              });
            }
          });

      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire({
          title: "Cancelled",
          text: "Deletion process was cancelled.",
          icon: "error"
        });
      }
    });
  }

}