﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scorecard.Data;
using scorecard.Models;

namespace scorecard.Controllers
{
    [Authorize]
    public class CriteriaController : Controller
    {
        // POST: Criteria/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(HomeIndexViewModel model)
        {
            StatusUpdateViewFailedModel failureModel = new StatusUpdateViewFailedModel();
            failureModel.Message = "Unspecified Error.";

            if (ModelState.IsValid)
            {
                RAGContext context = new RAGContext();

                // get the current user
                string currentUserId = User.Identity.GetUserId();
//                ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
                ApplicationUser currentUser = context.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

                if (currentUser != null)
                {
                    // find the criteria we are updating
                    Criteria criteria = context.Criteria.Find(model.UpdateId);

                    if (criteria != null)
                    {
                        try
                        {
                            // if we have text and a state change, save both
                            if (model.UpdateStatus != criteria.State)
                            {
                                // save the state change
                                StatusUpdate state = new StatusUpdate();
                                state.Criteria = criteria;
                                state.StateFrom = criteria.State;
                                state.StateTo = model.UpdateStatus;
                                state.User = currentUser;// TODO: This isn't saving for some reason
                                state.Stamp = DateTime.Now;
                                criteria.Updates.Add(state);
                            }

                            // save the update text
                            StatusUpdate update = new StatusUpdate();
                            update.Criteria = criteria;
                            update.StateFrom = model.UpdateStatus;
                            update.StateTo = model.UpdateStatus;
                            update.Text = model.UpdateText;
                            update.User = currentUser;// TODO: This isn't saving for some reason
                            update.Stamp = DateTime.Now;
                            criteria.Updates.Add(update);

                            context.SaveChanges();

                            // return view
                            StatusUpdateViewModel partialModel = new StatusUpdateViewModel(update);
                            return PartialView("_StatusUpdatePartial", partialModel);
                        }
                        catch(Exception ex)
                        {
                            failureModel.Exception = ex;
                        }
                    }
                    else
                    {
                        failureModel.Message = "Unable to find Criteria.";
                    }
                }
                else
                {
                    failureModel.Message = "Unable to find ApplicationUser.";
                }
            }

            return PartialView("_StatusUpdateFailedPartial", failureModel);
        }
    }
}